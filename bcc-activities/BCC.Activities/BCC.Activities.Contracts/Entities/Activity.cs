using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using BCC.EntityApi;

namespace BCC.Activities.Contracts.Entities
{
    [DisplayColumn("Title", "Start", true)]
    public class Activity : BaseEntity
    {
        /* Relationships */
        [Required]
        public Guid OrganiserId { get; set; }

        [Required]
        public Guid LocationId { get; set; }
        public virtual Location Location { get; }

        /* Attributes */
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        
        [StringLength(50)]
        public string Subtitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        public Types.ActivityStatus Status { get; set; }

        /* Dates */
        [Required]
        public DateTimeOffset Start { get; set; }

        [Required]
        public DateTimeOffset End { get; set; }

        public IList<Types.TimeSlot> TimeSlots { get; set; }
    }
}
