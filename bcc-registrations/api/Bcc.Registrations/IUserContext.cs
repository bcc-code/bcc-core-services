using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bcc.Registrations
{
    public interface IUserContext
    {
        IEnumerable<Claim> Claims { get;  }

        string UserId { get;  }

    }
}
