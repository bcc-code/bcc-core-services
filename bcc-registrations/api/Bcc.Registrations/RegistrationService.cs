using Bcc.Registrations.Requests;
using Bcc.Registrations.Services;
using System;
using System.Threading.Tasks;



namespace Bcc.Registrations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserContext _user;


        public RegistrationService(IUserContext user)
        {
            _user = user;
        }

        public Task<Registration> CreateRegistration(CreateRegistration request)
        {
            var registration = request.ToRegistration(new Guid(_user.UserId));
            // Create registration
            throw new NotImplementedException();
        }

        public Task<Registration> GetRegistration(Guid registrationId)
        {
            return Task.FromResult(default(Registration));
        }
    }
}
