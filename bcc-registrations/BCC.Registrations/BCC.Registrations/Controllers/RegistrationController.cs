using BCC.EntityApi;
using BCC.Registrations.Contracts.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BCC.Registrations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : EntityController<Registration>
    {
        public RegistrationController(DbContext context, EntityPolicy<Registration> policy)
         : base(context, policy)
        {
        }
    }
}
