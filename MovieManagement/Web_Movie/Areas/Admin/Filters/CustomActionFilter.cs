using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_Movie.Areas.Admin.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) //OnActionExecuted: sau khi quá trình thực thi
        {

        }

        public void OnActionExecuting(ActionExecutingContext context) //OnActionExecuting:  trong quá trình thực thi 
        {
            //var area = context.RouteData.Values["area"];
            //// Kiểm tra nếu không phải trang Login và thuộc khu vực Admin
            //if (context.RouteData.Values["action"].ToString() != "Login" && area?.ToString() == "Admin") // nếu như các trang người dùng gõ khác trang login và khác trang client
            //{
            //    if (!context.HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        context.Result = new RedirectResult("/Admin/Account/Account");
            //    }
            //}
            var area = context.RouteData.Values["area"];
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // Bỏ qua kiểm tra khi người dùng đang ở trang đăng nhập
            if (area?.ToString() == "Admin" && controller != "Account" && action != "Account")
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated) //check xem có đăng nhập hay chưa
                {
                    context.Result = new RedirectToActionResult("Account", "Account", new { area = "Admin" });
                }
            }
        }
    }
}
