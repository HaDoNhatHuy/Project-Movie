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
        public async Task<IActionResult> Index(Guid? ParentId)
        {
            if (ParentId == Guid.Empty)
                ParentId = null;

            var categories = await _context.Categories
                .Where(c => c.ParentId == ParentId)
                .ToListAsync();

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
