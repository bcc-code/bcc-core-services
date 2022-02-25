namespace BuildingBlocks.Api.Logging
{
    public interface ILogService
    {
        void TrackEvent(string eventName, IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null);

        void TrackException(Exception exception, IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null);

        void TrackMetric(string name, double value, IDictionary<string, string> properties = null);

        void TrackPageView(string pageView);
    }
}