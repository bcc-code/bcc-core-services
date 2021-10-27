using System;
using System.ComponentModel.DataAnnotations;

namespace BCC.Activities.Contracts.Types
{
    public class TimeSlot
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }
        [Required]
        public DateTimeOffset End { get; set; }
    }
}
