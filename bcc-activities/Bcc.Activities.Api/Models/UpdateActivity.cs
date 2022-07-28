using Bcc.Activities.Api.Models.Enums;

namespace Bcc.Activities.Api.Models;

public class UpdateActivity
{
    public Guid ActivityId { get; set; } 
    public DateTime StartDateTimeUtc { get; set; }
    public DateTime EndDateTimeUtc { get; set; }
    public string ReferenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Location { get; set; }
    /// <summary>
    /// BCC PersonId
    /// </summary>
    public int ResponsibleId { get; set; }
    public int NeededParticipants { get; set; }
    public ActivityAvailableFor AvailableFor { get; set; }
}