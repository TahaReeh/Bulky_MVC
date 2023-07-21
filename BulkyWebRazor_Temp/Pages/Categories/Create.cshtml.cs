using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public Category Categoryobj { get; set; }
        public CreateModel(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Categoryobj.Name == Categoryobj.DisplayOrder.ToString())
            {
                ModelState.TryAddModelError("", "Name And Display Order Cannot Be The Same");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(Categoryobj);
                db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
