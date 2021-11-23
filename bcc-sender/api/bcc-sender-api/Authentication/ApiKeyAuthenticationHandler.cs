using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bcc_sender_api.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IConfiguration _configuration;
        
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration
            
        )
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var apiKeyFromConfiguration = _configuration["ApiKey"];
            if (Request.Headers.ContainsKey("x-access-token") == false)
            {
                return Task.FromResult(AuthenticateResult.Fail("x-access-token header missing."));
            }
 
            var xAccessToken = Request.Headers["x-access-token"].ToString();

            if (xAccessToken != apiKeyFromConfiguration)
            {
                return Task.FromResult(AuthenticateResult.Fail("The Api Key is invalid"));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>(), "x-access-token"));
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}