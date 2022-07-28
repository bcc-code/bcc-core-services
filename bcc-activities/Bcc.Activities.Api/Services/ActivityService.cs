using AutoMapper;
using Bcc.Activities.Api.Models;
using BuildingBlocks.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Bcc.Activities.Api.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> GetActivitiesForOrganization(int organizationId);
        Task AddActivity(CreateActivity activity, CancellationToken cancellationToken);
        Task UpdateActivity(UpdateActivity activity, CancellationToken cancellationToken);
        Task<bool> DeleteActivity(Guid activityId);
    }

    public class ActivityService: IActivityService
    {
        private IMongoCollection<ActivityDocument> Collection { get; }
        private readonly IMapper _mapper;
        
        public ActivityService(IMongoService mongoService, IMapper mapper)
        {
            Collection = mongoService.GetCollection<ActivityDocument>();
            _mapper = mapper;
        }
        public async Task<List<Activity>> GetActivitiesForOrganization(int organizationId)
        {
            var filter = Builders<ActivityDocument>.Filter.Where(x => x.OwnerOrganizationId == organizationId);

            var activities = await Collection
                .AsQueryable()
                .Where(x => filter.Inject())
                .ToListAsync();

            return activities.ToList().Select(x => _mapper.Map<ActivityDocument, Activity>(x)).ToList();
        }

        public async Task AddActivity(CreateActivity activity, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<CreateActivity, ActivityDocument>(activity);
            
            await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);
        }

        public Task UpdateActivity(UpdateActivity activity, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            
        }

        public Task<bool> DeleteActivity(Guid activityId)
        {
            return Task.FromResult(true);
        }
    }
}