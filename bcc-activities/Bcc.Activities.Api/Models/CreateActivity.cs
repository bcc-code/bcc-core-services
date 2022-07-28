using Bcc.Activities.Api.Models.Enums;

namespace Bcc.Activities.Api.Models;

public class CreateActivity
{
    public Guid ActivityId { get; set; }
    public DateTime StartDateTimeUtc { get; set; }
    public DateTime EndDateTimeUtc { get; set; }
    public string Name { get; set; } = null!;
    
    public string ReferenceNumber { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    /// <summary>
    /// BCC PersonId
    /// </summary>
    public int ResponsibleId { get; set; }
    public int NeededParticipants { get; set; }
    
    public ActivityAvailableFor AvailableFor { get; set; }
    /// <summary>
    /// Owner of an activity, TenantKey
    /// </summary>
    public string TenantOwner { get; set; }
}