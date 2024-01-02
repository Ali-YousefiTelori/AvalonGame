using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Avalon.WebApp.Attributes;

public class AvalonSecurityAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Customize your authorization logic here
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            // Not logged in
            context.Result = new UnauthorizedResult();
            return;
        }

        // Check for the specific permission
        if (!user.HasClaim(c => c.Type == "UniqueIdentity"))
        {
            // User does not have the required permission
            context.Result = new ForbidResult();
        }
    }
}