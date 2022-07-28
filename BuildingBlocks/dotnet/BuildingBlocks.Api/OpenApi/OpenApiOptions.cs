namespace BuildingBlocks.Api.OpenApi
{
    public class OpenApiOptions
    {
        public string? Title { get; set; } 
        public string? Version { get; set; }
        public string? AuthenticationType { get; set; }
        public Dictionary<string, string> Scopes { get; set; } = new();
    }
}