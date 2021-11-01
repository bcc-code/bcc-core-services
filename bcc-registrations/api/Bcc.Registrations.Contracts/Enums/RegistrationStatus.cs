using System;
using System.Collections.Generic;
using System.Text;

namespace Bcc.Registrations
{
    [Flags]
    public enum RegistrationStatus
    {
        Created =   0,
        Confirmed = 1,
        Revoked =   2,
        Pending =   16        
    }
}
