using Bcc.Registrations.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bcc.Registrations.Services
{
    public interface IRegistrationService
    {
        Task<Registration> CreateRegistration(CreateRegistration request);

        Task<Registration> GetRegistration(Guid registrationId);
    }
}
