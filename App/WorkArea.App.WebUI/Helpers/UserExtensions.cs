using System;
using System.Security.Claims;
using System.Security.Principal;

namespace WorkArea.App.WebUI.Helpers
{
    public static class UserExtensions
    {
        public static int GetUserId(this IPrincipal user)
        {
            var userClaim = user as ClaimsPrincipal;
            return Convert.ToInt32(userClaim?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}