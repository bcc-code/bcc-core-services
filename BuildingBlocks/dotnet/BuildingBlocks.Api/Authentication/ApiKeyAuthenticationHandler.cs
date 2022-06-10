using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Api.Authentication
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
          
            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(xAccessToken));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 4);
            var apiKeyFromRequest = authSplit[0];
            
            int.TryParse(authSplit[1], out var personId);
            int.TryParse(authSplit[2], out var userSpouseId);
            int.TryParse(authSplit[3], out var userChurchId);
 
            if (apiKeyFromRequest != apiKeyFromConfiguration)
            {
                return Task.FromResult(AuthenticateResult.Fail("The Api Key is invalid"));
            }

            var claims = new List<Claim>();
            if (personId > 0)
            {
                claims.Add(new Claim(Claims.UserId, personId.ToString())); 
            }
            if (userChurchId > 0)
            {
                claims.Add(new Claim(Claims.Church, userChurchId.ToString())); 
            }

            if (userSpouseId > 0)
            {
                claims.Add(new Claim(Claims.SpouseId, userSpouseId.ToString())); 
            }
            
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, WebAuthenticationType.Auth0));
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}