using Bcc.Registrations.Requests;
using Bcc.Registrations.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bcc.Registrations.Client
{
    public class RegistrationsClient : ApiHttpClient, IRegistrationService
    {

        public RegistrationsClient(IHttpClientFactory clientFactory, ApiClientOptions options) : base(clientFactory, options)
        {           
        }

        public Task<Registration> CreateRegistration(CreateRegistration request)
        {
            return Post<CreateRegistration, Registration>("registrations", request);
        }

        public Task<Registration> GetRegistration(Guid registrationId)
        {
            return Get<Registration>($"registrations/{registrationId}");
        }
    }
}
