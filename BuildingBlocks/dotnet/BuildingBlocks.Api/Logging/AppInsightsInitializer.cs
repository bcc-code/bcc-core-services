using BuildingBlocks.Api.Authentication;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BuildingBlocks.Api.Logging
{
    public class AppInsightsInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITelemetryParameters _telemetryParameters;
        private readonly IUser _user;

        public AppInsightsInitializer(IHttpContextAccessor httpContextAccessor, IUser user,
            ITelemetryParameters telemetryParameters)
        {
            _httpContextAccessor = httpContextAccessor;
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _telemetryParameters = telemetryParameters;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var properties = new Dictionary<string, string>
            {
                {"AppVersion", _telemetryParameters.AppVersion()}
            };

            if (string.IsNullOrEmpty(_user.GetClaimsIdentity(Claims.OrganizationId)) == false)
            {
                properties.Add("OrganizationId", _user.GetClaimsIdentity(Claims.OrganizationId));
            }

            if (string.IsNullOrEmpty(_user.GetClaimsIdentity(Claims.Age)) == false)
            {
                properties.Add("Age", _user.GetClaimsIdentity(Claims.Age));
            }

            if (telemetry is RequestTelemetry requestTelemetry && ExcludeUrls.Contains(requestTelemetry.Url.AbsolutePath))
            {
                return;
            }

            var to = new TelemetryObject
            {
                UserId = _user.Id.ToString(),
                AccountId = _user.OrganizationId.ToString(),
                UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"] ?? new StringValues(),
                Properties = properties,
                SystemVersion = _telemetryParameters.SystemVersion()
            };

            TelemetryHelper.Create(to).Initialize(telemetry);
        }

        private IEnumerable<string> ExcludeUrls
        {
            get
            {
                return new string[]
                {
                    "/swagger",
                    "/health",
                    "/server-status"
                };
            }
        }
    }
}