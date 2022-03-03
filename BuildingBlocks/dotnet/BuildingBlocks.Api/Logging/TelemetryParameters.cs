using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Api.Logging
{
    public class TelemetryParameters : ITelemetryParameters
    {
        private readonly IConfiguration _configuration;

        public TelemetryParameters(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AppVersion()
        {
            return Assembly.GetExecutingAssembly().FullName;
        }

        public string ClientName()
        {
            return string.Empty;
        }

        public string InstrumentationKey()
        {
            return _configuration.GetConnectionString("ApplicationInsights");
        }

        public string SystemVersion()
        {
            return Logging.SystemVersion.Get(_configuration["version:mainVersion"], _configuration["version:buildVersion"]);
        }
    }
}