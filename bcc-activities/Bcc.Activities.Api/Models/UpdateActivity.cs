using System.ComponentModel.DataAnnotations;

namespace Bcc.Activities.Api.Models;

public class UpdateActivity
{
    public Guid ActivityId { get; set; } 
    [Required]
    public DateTime StartDateTimeUtc { get; set; }
    [Required]
    public DateTime EndDateTimeUtc { get; set; }
    public string Reference { get; set; }
    [Required]
    public string Name { get; set; } = null!;
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