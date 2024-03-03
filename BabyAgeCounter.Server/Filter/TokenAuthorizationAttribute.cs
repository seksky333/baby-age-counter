using BabyAgeCounter.Server.Filter;
using BabyAgeCounter.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BabyAgeCounter.Server.Filter;

public class TokenAuthorizationAttribute : TypeFilterAttribute
{
    public TokenAuthorizationAttribute() : base(typeof(TokenAuthorizationFilter))
    {
    }
}