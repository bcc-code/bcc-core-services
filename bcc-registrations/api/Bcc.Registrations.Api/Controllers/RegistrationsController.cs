using Bcc.Registrations.Requests;
using Bcc.Registrations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bcc.Registrations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase, IRegistrationService
    {
        private readonly IRegistrationService _registrations;

        public RegistrationsController(IRegistrationService registrations)
        {
            _registrations = registrations;
        }

        [HttpPost]
        [Route("")]
        public Task<Registration> CreateRegistration(CreateRegistration request)
        {
            return _registrations.CreateRegistration(request);
        }

        [HttpGet]
        [Route("{registrationId}")]
        public Task<Registration> GetRegistration(Guid registrationId)
        {
            return _registrations.GetRegistration(registrationId);
        }
    }
}
