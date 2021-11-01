using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bcc.Registrations
{
    public class Registration : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /* Relationships */
        [Required]
        public Guid ActivityId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Tentent where registration takes place (not necessarily user's "primary" tentant or event organizer)
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /* Attributes */
        [Required]
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Created;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
