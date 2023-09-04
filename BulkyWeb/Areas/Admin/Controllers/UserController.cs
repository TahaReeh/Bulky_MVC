using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagment(string userId)
        {
            RoleManagmentVM RoleVM = new RoleManagmentVM()
            {
                applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Company"),

                RoleList = _roleManager.Roles.Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Name
                    }
                    ),
                CompanyList = _unitOfWork.Company.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                    )
            };

            RoleVM.applicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser
                .Get(u => u.Id == userId)).GetAwaiter().GetResult().FirstOrDefault();

            return View(RoleVM);
        }


        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM)
        {
            ApplicationUser objApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagmentVM.applicationUser.Id);
            string oldRole = _userManager.GetRolesAsync(objApplicationUser).GetAwaiter().GetResult().FirstOrDefault();
            
            if (!(roleManagmentVM.applicationUser.Role == oldRole))
            {
                if (roleManagmentVM.applicationUser.Role == SD.Role_Company)
                {
                    objApplicationUser.CompanyId = roleManagmentVM.applicationUser.CompanyId;
                }
                if (oldRole == SD.Role_Company)
                {
                    objApplicationUser.CompanyId = null;
                }
                _unitOfWork.ApplicationUser.Update(objApplicationUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(objApplicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(objApplicationUser, roleManagmentVM.applicationUser.Role).GetAwaiter().GetResult();
            }
            else
            {
                if (oldRole == SD.Role_Company && objApplicationUser.CompanyId != roleManagmentVM.applicationUser.CompanyId)
                {
                    objApplicationUser.CompanyId = roleManagmentVM.applicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(objApplicationUser);
                    _unitOfWork.Save();
                }
            }

            TempData["success"] = "Permission updated successfully";
            return RedirectToAction(nameof(Index));
        }


        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

            foreach (var user in objUserList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

                //if company is null
                user.Company ??= new()
                {
                    Name = "-"
                };
            }
            return Json(new { data = objUserList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while locking/Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
