using System;
using System.Collections.Generic;
using System.Text;

namespace Bcc.Registrations.Entities
{
    public class Group
    {
        public Guid TenantId { get; set; }

        public GroupCondition[] Conditions { get; set; }


    }
}
