using Bcc.Registrations.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcc.Registrations
{
    public static class RegistrationExtensions
    {
        public static Registration ToRegistration(this CreateRegistration requst, Guid userId)
        {
            var now = DateTimeOffset.Now;
            return new Registration
            {
                ActivityId = requst.ActivityId,
                CreatedAt = now,
                Id = requst.Id,
                Status = RegistrationStatus.Created,
                UpdatedAt = now,
                TenantId = requst.TenantId,
                PersonId = requst.PersonId,
                CreatedById = userId
            };
        }
    }
}
