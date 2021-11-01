using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bcc.Registrations.Requests
{
    public class CreateRegistration 
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ActivityId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public Guid TenantId { get; set; }
    }

}
