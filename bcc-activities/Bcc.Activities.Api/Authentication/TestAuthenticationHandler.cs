using System.Security.Claims;
using System.Text.Encodings.Web;
using BuildingBlocks.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Bcc.Activities.Api.Authentication;

public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor httpContextAccessor) : base(options,
        logger, encoder, clock)
    {
        _httpContextAccessor = httpContextAccessor;
        if (_httpContextAccessor == null)
        {
            throw new ArgumentNullException(nameof(IHttpContextAccessor));
        }
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new ArgumentNullException(nameof(_httpContextAccessor.HttpContext));
        }

        var claims = new List<Claim>();
        _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(TestHeaders.UserId, out var personId);
        if (string.IsNullOrEmpty(personId) == false)
        {
            claims.Add(new Claim(Claims.PersonId, personId.ToString()));
        }
        _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(TestHeaders.OrganizationId, out var organizationId);
        if (string.IsNullOrEmpty(organizationId) == false)
        {
            claims.Add(new Claim(Claims.OrganizationId, organizationId.ToString()));
        }
        _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(TestHeaders.TeamId, out var teamId);
        if (string.IsNullOrEmpty(teamId) == false)
        {
            claims.Add(new Claim(Claims.TeamId, teamId.ToString()));
        }
        var userRoles = _httpContextAccessor.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == TestHeaders.Role).Value;
        if (userRoles.Count > 0)
        {
            claims.AddRange(userRoles.ToString().Split('|').Select(role => new Claim(Claims.Role, role)));
        }
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, WebAuthenticationType.Test));
            
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
    }
}

public static class TestHeaders
{
    public const string OrganizationId = nameof(OrganizationId);
    public const string UserId = nameof(UserId);
    public const string Role = nameof(Role);
    public const string TeamId = nameof(TeamId);
}