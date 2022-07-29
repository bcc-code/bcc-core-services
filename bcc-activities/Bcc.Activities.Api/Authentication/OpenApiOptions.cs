namespace Bcc.Activities.Api.Authentication;

public class OpenApiOptions
{
    public string Title { get; set; } 
    public string Version { get; set; }
    public string AuthenticationType { get; set; }
}
    
public static class OpenApiOptionsExtensions
{
    public static OpenApiOptions GetOpenApiOptions(this IConfiguration configuration)
    {
        var options = configuration.GetSection("Api").Get<OpenApiOptions>();
        if (options == null)
        {
            throw new ArgumentNullException(nameof(OpenApiOptions));
        }
        if (string.IsNullOrEmpty(options.Title))
        {
            throw new ArgumentNullException(nameof(options.Title));
        }
        if (string.IsNullOrEmpty(options.Version))
        {
            throw new ArgumentNullException(nameof(options.Version));
        }
        if (string.IsNullOrEmpty(options.AuthenticationType))
        {
            throw new ArgumentNullException(nameof(options.AuthenticationType));
        }

        return options;
    }
}

