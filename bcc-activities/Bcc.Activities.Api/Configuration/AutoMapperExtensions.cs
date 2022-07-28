using Bcc.Activities.Api.Models;
using Bcc.Activities.Api.Services;

namespace Bcc.Activities.Api.Configuration;

public static class AutoMapperExtensions
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(expression =>
        {
            expression.CreateMap<ActivityDocument, Activity>();
            expression.CreateMap<CreateActivity, ActivityDocument>();
            expression.CreateMap<UpdateActivity, ActivityDocument>();
        });
    }
}