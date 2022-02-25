using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace BuildingBlocks.Api.Logging
{
    public class TelemetryHelper
    {
        private readonly TelemetryObject _to;

        private TelemetryHelper(TelemetryObject to)
        {
            _to = to;
        }

        public static TelemetryHelper Create(TelemetryObject to)
        {
            return new TelemetryHelper(to);
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Session.Id = _to.SessionId;
            telemetry.Context.User.Id = _to.UserId;
            telemetry.Context.User.AuthenticatedUserId = _to.UserId;
            telemetry.Context.User.AccountId = _to.AccountId;

            if (string.IsNullOrWhiteSpace(_to.UserAgent) == false) telemetry.Context.User.UserAgent = _to.UserAgent;

            telemetry.Context.Location.Ip = _to.RemoteIpAddress;

            if (telemetry is ISupportProperties supportProperties)
                foreach (var keyValuePair in _to.Properties)
                    supportProperties.Properties[keyValuePair.Key] = keyValuePair.Value;

            if (string.IsNullOrEmpty(_to.SystemVersion) == false)
                telemetry.Context.Component.Version = _to.SystemVersion;

            if (string.IsNullOrEmpty(_to.InstrumentationKey) == false)
                telemetry.Context.InstrumentationKey = _to.InstrumentationKey;

            var requestTelemetry = telemetry as RequestTelemetry;
            if (requestTelemetry == null) return;

            var parsed = int.TryParse(requestTelemetry.ResponseCode, out var code);
            if (parsed == false) return;

            if (code >= 400 && code < 500)
            {
                requestTelemetry.Success = true;
                requestTelemetry.Properties.Add("Overridden400s", "true");
            }
        }
    }

    public class TelemetryObject
    {
        public string AccountId { get; set; }
        public DeviceContext Device { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public string? RemoteIpAddress { get; set; }
        public string SessionId { get; set; }
        public string UserAgent { get; set; }
        public string UserId { get; set; }
        public string SystemVersion { get; set; }
        public string InstrumentationKey { get; set; }
    }
}