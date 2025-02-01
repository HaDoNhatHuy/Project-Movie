using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        public MovieController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var movieList = _context.Movies.Include(m => m.Category).ToList();
            return View(movieList);
        }
    }
}
