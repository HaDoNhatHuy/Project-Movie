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
        private IWebHostEnvironment _webHostEnvironment;
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager, MovieContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> MyAccount()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = currentUser;
            return View();
        }
        public async Task<IActionResult> Settings()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = currentUser;
            return View();
        }
        public async Task<IActionResult> UpdateProfile(AppUserModel userModel, IFormFile Avatar)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var updateUser = await _userManager.FindByEmailAsync(currentUser.Email);
            if (updateUser == null)
            {
                return NotFound();
            }
            updateUser.FullName = userModel.FullName;
            updateUser.Avatar = userModel.Avatar;
            updateUser.Address = userModel.Address;
            updateUser.PhoneNumber = userModel.PhoneNumber;
            if (Avatar != null)
            {
                var path = Path.Combine(this._webHostEnvironment.WebRootPath, "assets/admin/images/UserImages/", Avatar.FileName);//lấy đường dẫn hình
                                                                                                                                  //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await Avatar.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                updateUser.Avatar = Avatar.FileName;
            }
            //Kiểm tra mk hiện tại nếu cần thay đổi mk
            if (!string.IsNullOrEmpty(userModel.PasswordHash))
            {
                var passwordHasher = new PasswordHasher<IdentityUser>();

                //kiểm tra mk hiện tại
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, userModel.PasswordHash);
                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    TempData["error"] = "Mật khẩu hiện tại không chính xác";
                    return RedirectToAction("Settings", "Account");
                }
                //kiểm tra mk mới và xác nhận mk mới
                if (!string.IsNullOrEmpty(userModel.NewPassword) && !string.IsNullOrEmpty(userModel.ConfirmPassword))
                {
                    if (userModel.NewPassword == userModel.PasswordHash)
                    {
                        TempData["error"] = "Mật khẩu mới không được trùng với mật khẩu hiện tại.";
                        return RedirectToAction("Settings", "Account");
                    }
                    if (userModel.NewPassword != userModel.ConfirmPassword)
                    {
                        TempData["error"] = "Xác nhận mật khẩu không khớp với mật khẩu mới.";
                        return RedirectToAction("Settings", "Account");
                    }
                    currentUser.PasswordHash = passwordHasher.HashPassword(currentUser, userModel.NewPassword);
                }
                else
                {
                    TempData["error"] = "Mật khẩu mới và xác nhận mật khẩu không được để trống.";
                    return RedirectToAction("Settings", "Account");
                }
            }
            // Cập nhật thông tin người dùng
            var result = await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                TempData["error"] = "Có lỗi xảy ra khi cập nhật tài khoản.";
                return RedirectToAction("Settings", "Account");
            }            
            await _userManager.UpdateAsync(updateUser);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật tài khoản thành công";
            return RedirectToAction("Settings", "Account");
        }
    }
}