namespace BuildingBlocks.Api.OpenApi
{
    public class OpenApiOptions
    {
        public string ApiTitle { get; } = null!;
        public string ApiVersion { get; } = null!;
        public string AuthenticationType { get; set; } = null!;
    }
}