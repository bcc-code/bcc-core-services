using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BuildingBlocks.Api.Authentication;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares;

public class AddingCustomClaimsMiddleware
{
    private readonly RequestDelegate _next;

    public AddingCustomClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var claims = new List<Claim>();

        var userMetaDataClaim = httpContext.User?.Claims.SingleOrDefault(c => c.Type == Claims.UserId);
        var churchMetaDataClaim = httpContext.User?.Claims.SingleOrDefault(c => c.Type == Claims.Church);

        await _next(httpContext);
    }
}