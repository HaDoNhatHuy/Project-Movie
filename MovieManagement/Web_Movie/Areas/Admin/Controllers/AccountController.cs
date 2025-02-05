using Database_Movie.EF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System.Security.Claims;
using Web_Movie.Areas.Admin.ViewModels;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;
        private readonly MovieContext _context;
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager, MovieContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Account(string returnUrl)
        {
            return View(new AccountViewModel
            {
                LoginViewModel = new LoginViewModel { ReturnUrl = returnUrl },
                RegisterViewModel = new RegisterViewModel()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.LoginViewModel.Email);
            if (user == null)
            {
                return View("Account", new AccountViewModel
                {
                    LoginViewModel = loginVM.LoginViewModel,
                    RegisterViewModel = new RegisterViewModel()
                });
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginVM.LoginViewModel.Password, false, false);
            if (result.Succeeded)
            {                
                return Redirect(loginVM.LoginViewModel.ReturnUrl ?? "/Admin");
            }
            return View("Account", new AccountViewModel
            {
                LoginViewModel = loginVM.LoginViewModel,
                RegisterViewModel = new RegisterViewModel()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel registerVM)
        {
            var newUser = new AppUserModel { FullName = registerVM.RegisterViewModel.FullName, Email = registerVM.RegisterViewModel.Email };
            var result = await _userManager.CreateAsync(newUser, registerVM.RegisterViewModel.Password);

            if (result.Succeeded)
            {
                TempData["success"] = "Registration successful!";
                return RedirectToAction("Account");
            }


            // Re-populate the view model with login data
            var model = new AccountViewModel
            {
                LoginViewModel = new LoginViewModel(),
                RegisterViewModel = registerVM.RegisterViewModel
            };

            return View("Account", model);
        }
        public async Task<IActionResult> Logout(string returnUrl = "/Admin")
        {
            await HttpContext.SignOutAsync();
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
        public IActionResult MyAccount()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }
    }
}