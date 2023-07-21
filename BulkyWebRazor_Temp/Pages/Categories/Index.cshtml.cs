using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            CategoryList = db.Categories.ToList();
        }
    }
}
