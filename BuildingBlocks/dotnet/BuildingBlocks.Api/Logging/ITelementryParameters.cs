namespace BuildingBlocks.Api.Logging
{
    public interface ITelemetryParameters
    {
        string ClientName();

        string AppVersion();

        string SystemVersion();

        string InstrumentationKey();
    }
}