using ElnityServerDAL.Constant;
using ElnityServerDAL.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElnityServer.Authorization.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        private readonly List<string> _roles = new List<string>();

        public AuthorizeAttribute()
        {
            _roles.Add(SecuritySettings.DefaultRoleForProject);
        }

        public AuthorizeAttribute(string roles)
        {
            string[] required_roles = roles.Split(",");

            foreach (string role in required_roles)
            {
                _roles.Add(role);
            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (ApplicationUser)context.HttpContext.Items["User"];
            var userRoles = (List<string>)context.HttpContext.Items["Roles"];

            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
               bool userHasRole = userRoles.Any(role => _roles.Contains(role));
               if (!userHasRole)
                    context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }

        }
    }
}
