using BuildingBlocks.MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace Bcc.Activities.Api.Services;

[CollectionName("activities")]
public class ActivityDocument
{
    [BsonId]
    public Guid ActivityId { get; set; }
    public DateTime StartDateTimeUtc { get; set; }
    public DateTime EndDateTimeUtc { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime LastChangedTimeUtc { get; set; }
    public int CreatedById { get; set; }
    public int ResponsibleId { get; set; }
    public string ResponsibleName { get; set; }
    public int NeededParticipants { get; set; }
    public int OwnerOrganizationId { get; set; }
}