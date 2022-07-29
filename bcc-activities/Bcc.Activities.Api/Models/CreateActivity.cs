using System.ComponentModel.DataAnnotations;

namespace Bcc.Activities.Api.Models;

public class CreateActivity
{
    public Guid ActivityId { get; set; }
    [Required]
    public DateTime StartDateTimeUtc { get; set; }
    public DateTime EndDateTimeUtc { get; set; }
    public string Name { get; set; } = null!;
    public string Reference { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    /// <summary>
    /// BCC PersonId
    /// </summary>
    public int ResponsibleId { get; set; }
    public int NeededParticipants { get; set; }
    /// <summary>
    /// Owner of an activity, OrganizationId
    /// </summary>
    public int OwnerOrganization { get; set; }
}