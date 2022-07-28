using AutoMapper;
using Bcc.Activities.Api.Models;
using Bcc.Activities.Api.Services;

namespace Bcc.Activities.Tests.Activities;

public class FakeActivityService : IActivityService
{
    private readonly IMapper _mapper;

    public FakeActivityService(IMapper mapper)
    {
        _mapper = mapper;
    }
    protected static List<ActivityDocument> Activities = new List<ActivityDocument>();
    
    public Task<List<Activity>> GetActivitiesForOrganization(int organizationId)
    {
        var activities = _mapper.Map<List<ActivityDocument>, List<Activity>>(Activities);
        return Task.FromResult<List<Activity>>(activities);
    }

    public Task AddActivity(CreateActivity activity, CancellationToken cancellationToken)
    {
        var activityToSave = _mapper.Map<CreateActivity, ActivityDocument>(activity);
        Activities.Add(activityToSave);
        return Task.CompletedTask;
    }

    public Task UpdateActivity(UpdateActivity activity, CancellationToken cancellationToken)
    {
        var document = Activities.FirstOrDefault(x => x.ActivityId == activity.ActivityId);
        if (document != null)
        {
            _mapper.Map<UpdateActivity, ActivityDocument>(activity, document);
        }

        return Task.CompletedTask;
    }

    public Task<bool> DeleteActivity(Guid activityId)
    {
        var document = Activities.FirstOrDefault(x => x.ActivityId == activityId);
        return Task.FromResult(document != null && Activities.Remove(document));
    }
}