using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public NewsController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var news = _context.News.Include(i => i.Category).ToList();
            return View(news);
        }
        public IActionResult Create()
        {
            var categoryList = _context.Categories
                .Where(i => i.ParentId == Guid.Parse("0922c247-a6dc-42aa-855b-42bdfb6926e1"))
                .ToList();
            ViewBag.Categories = new SelectList(categoryList, "CategoryId", "CategoryName");
            return View();
        }
    }
}
