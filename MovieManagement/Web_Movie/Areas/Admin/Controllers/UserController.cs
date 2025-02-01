using Database_Movie.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly MovieContext _context;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(MovieContext context, UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
            //var userWithRoles = await (from u in _context.Users
            //                           join ur in _context.UserRoles on u.Id equals ur.UserId
            //                           join r in _context.Roles on ur.RoleId equals r.Id
            //                           select new { User = u, RoleName = r.Name }).ToListAsync();
            //var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewBag.LoggedInUser = loggedInUserId;
            //return View(userWithRoles);
        }
    }
}
