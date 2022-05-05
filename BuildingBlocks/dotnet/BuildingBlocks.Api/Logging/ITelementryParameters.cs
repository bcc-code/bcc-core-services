namespace BuildingBlocks.Api.Logging
{
    public interface ITelemetryParameters
    {
        string AppVersion();

        string SystemVersion();
    }
}