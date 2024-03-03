using BabyAgeCounter.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BabyAgeCounter.Server.Filter;

public class TokenAuthorizationFilter(IHttpContextAccessor httpContextAccessor, IUserService userService)
    : IAsyncAuthorizationFilter
{
    const string TokenHeader = "Bearer ";

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authToken = httpContextAccessor?.HttpContext?.Request.Headers.Authorization.FirstOrDefault();
        
        if (string.IsNullOrEmpty(authToken))
        {
            context.Result = new UnauthorizedObjectResult("Authentication token is required!");
            return;
        }

        var remainingSize = (authToken.Length - TokenHeader.Length);
        var token = authToken.Substring(TokenHeader.Length, remainingSize);
        var isAuthenticated = await userService.Authenticate(token);
        
        if (isAuthenticated) return;
        context.Result = new UnauthorizedObjectResult("Invalid authentication token!");
    }
}