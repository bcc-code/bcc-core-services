using Microsoft.ApplicationInsights;

namespace BuildingBlocks.Api.Logging
{
    public class LogService : ILogService
    {
        private readonly TelemetryClient _telemetry;
        public LogService(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            _telemetry.TrackEvent(eventName, properties, metrics);
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            _telemetry.TrackException(exception, properties, metrics);
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            _telemetry.TrackMetric(name, value, properties);
        }

        public void TrackPageView(string pageView)
        {
            _telemetry.TrackPageView(pageView);
        }
    }
}