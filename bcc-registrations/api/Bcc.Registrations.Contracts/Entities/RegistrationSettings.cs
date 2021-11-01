using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bcc.Registrations.Entities
{
    public class RegistrationSettings
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MasterId { get; set; }

        public bool IsMaster => Id == MasterId || MasterId == Guid.Empty;

        public Guid ActivityId { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        public Guid[] AgentIds { get; set; }

        public bool AllowRegistration { get; set; }

        public Guid[] Visibility { get; set; }

        public DateTimeOffset OpenRegistration { get; set; }

        public DateTimeOffset Deadline { get; set; }

    }


    
}
