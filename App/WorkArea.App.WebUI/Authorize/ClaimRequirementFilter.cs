using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkArea.App.WebUI.Helpers;
using WorkArea.Application.DTOs;
using WorkArea.Application.Services;

namespace WorkArea.App.WebUI.Authorize
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimRequirementFilter(IHttpContextAccessor contextAccessor, UserService userService)
        {
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var nameIdentifierExist = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (nameIdentifierExist == null)
            {
                context.Result = new RedirectResult("/Auth/Login");
                return;
            }

            var session = new SessionHelper(_contextAccessor);
            var userSession = session.Get<UserSimpleDto>("UserInfo");

            var userId = Int32.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            if (userSession == null)
            {
                var user = _userService.GetUserSimpleInfo(userId).GetAwaiter().GetResult();
                if (user == null)
                {
                    context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Result = new RedirectResult("/Auth/Login");
                    return;
                }
                session.Set("UserInfo", user);
            }

            return;
        }
    }
}
