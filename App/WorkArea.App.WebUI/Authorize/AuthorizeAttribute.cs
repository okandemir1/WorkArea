using Microsoft.AspNetCore.Mvc;

namespace WorkArea.App.WebUI.Authorize
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() 
            : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { };
        }
    }
}
