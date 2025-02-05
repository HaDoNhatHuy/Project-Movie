using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly MovieContext _context;
        public HomeController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
