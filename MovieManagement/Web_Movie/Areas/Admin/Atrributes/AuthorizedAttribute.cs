using Database_Movie.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Web_Movie.Areas.Admin.Atrributes
{
    public class AuthorizedAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _role;
        public AuthorizedAttribute(string role)
        {
            _role = role;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            // Lấy UserManager từ DI
            var userManager = context.HttpContext.RequestServices.GetService(typeof(UserManager<AppUserModel>)) as UserManager<AppUserModel>;
            var dbContext = context.HttpContext.RequestServices.GetService(typeof(MovieContext)) as MovieContext;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userManager != null && dbContext != null)
            {
                var appUser = await userManager.FindByIdAsync(userId);
                if (appUser != null)
                {
                    // Lấy danh sách Role từ Group của User
                    var roles = await dbContext.GroupRoles
                        .Where(gr => gr.GroupId == appUser.GroupId)
                        .Select(gr => gr.Role.Name)
                        .ToListAsync();
                    if (!roles.Contains(_role))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }
        }
    }
}
