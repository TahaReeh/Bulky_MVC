using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void OnGet(int? id)
        {
            if (id != null)
                Category = db.Categories.Find(id);
        }
        public IActionResult OnPost()
        {
            if (Category == null)
                return NotFound();
            db.Categories.Remove(Category);
            db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
