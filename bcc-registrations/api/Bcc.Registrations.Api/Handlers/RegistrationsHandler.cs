using Bcc.Registrations.Requests;
using Bcc.Registrations.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bcc.Registrations.Api.Handlers
{
    public class RegistrationsHandler : IConsumer<CreateRegistration>
    {
        private readonly IRegistrationService _registrations;

        public RegistrationsHandler(IRegistrationService registrations)
        {
            this._registrations = registrations;
        }

        public async Task Consume(ConsumeContext<CreateRegistration> context)
        {
            var registration = await _registrations.CreateRegistration(context.Message);
            await context.RespondAsync(registration);
        }
    }
}
