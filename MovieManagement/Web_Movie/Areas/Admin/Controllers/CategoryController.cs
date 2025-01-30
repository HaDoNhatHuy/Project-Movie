using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly MovieContext _context;
        public CategoryController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index(Guid? parentId)
        {
            var categories = parentId == null
                ? _context.Categories.Where(c => c.ParentId == null).ToList()  // Danh mục gốc
                : _context.Categories.Where(c => c.ParentId == parentId).ToList();  // Danh mục con của parentId

            // Trả về danh sách categories cho view
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetChildCategory(Guid parentId)
        {
            var subCategories = await _context.Categories
                .Where(c => c.ParentId == parentId)
                .Select(c => new
                {
                    c.CategoryId,
                    c.CategoryName,
                    c.CreatedDate,
                    c.Status
                }).ToListAsync();

            return Json(subCategories);
        }
    }
}
