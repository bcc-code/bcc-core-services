using Bcc.Activities.Api.Configuration;
using BuildingBlocks.Api.Extensions;
using HostingExtensions = Bcc.Activities.Api.Configuration.HostingExtensions;

var builder = WebApplication.CreateBuilder(args);

HostingExtensions.AddCoreAppSettings(builder.Host);
builder.Services.AddDependencies(builder.Configuration, builder.Environment);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.ConfigureBlocks(builder.Configuration, builder.Environment);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

namespace Bcc.Activities.Api
{
    public partial class Program { }
}