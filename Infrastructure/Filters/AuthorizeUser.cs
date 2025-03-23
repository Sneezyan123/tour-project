using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TourProject.Persistence;

namespace TourProject.Infrastructure.Filters
{
    public class AuthorizeUserAttribute: AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _roles;
        private readonly string _policies;

        public AuthorizeUserAttribute(string roles = "", string policies = "")
        {
            _roles = roles;
            _policies = policies;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userId = user.FindFirstValue(ClaimTypes.Name);
            if(userId is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var dbcontext = httpContext.RequestServices.GetRequiredService<ApiDbContext>();
            var myUser = await dbcontext.Users.FindAsync(new Guid(userId));
            if(myUser is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            httpContext.Items["user"] = myUser;

            var authorizationService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            if (!string.IsNullOrEmpty(_roles))
            {
                var allowedRoles = _roles.Split(",");
                if (!allowedRoles.Any(role => user.IsInRole(role.Trim())))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(_policies))
            {
                var policyCheck = await authorizationService.AuthorizeAsync(user, null, _policies);
                if (!policyCheck.Succeeded)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

        }
    }
}
