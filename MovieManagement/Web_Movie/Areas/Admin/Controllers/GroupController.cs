using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        private readonly MovieContext _context;
        public GroupController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var group = _context.Groups.ToList();
            return View(group);
        }
    }
}
