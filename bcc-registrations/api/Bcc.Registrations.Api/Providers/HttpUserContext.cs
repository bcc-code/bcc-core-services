using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bcc.Registrations.Api.Providers
{
    public class HttpUserContext : IUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpUserContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Claim> Claims { get => httpContextAccessor.HttpContext.User.Claims; }
        public string UserId { get => httpContextAccessor.HttpContext.User.Identity.Name; }
    }
}
