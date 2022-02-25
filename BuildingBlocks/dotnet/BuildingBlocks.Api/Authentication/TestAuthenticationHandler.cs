using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyShare.Core.Api.Authentication;

namespace BuildingBlocks.Api.Authentication
{
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ClaimsPrincipal _id;

        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options,
            logger, encoder, clock)
        {
            _id = new ClaimsPrincipal(new ClaimsIdentity(WebAuthenticationType.Test));
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(
                AuthenticateResult.Success(new AuthenticationTicket(_id,
                    TestAuthenticationScheme.AuthenticationScheme)));
        }
    }
}