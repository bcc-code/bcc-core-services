using System;
using System.Collections.Generic;
using System.Text;

namespace Bcc.Registrations
{
    public interface IEntity
    {
        public Guid Id { get; }

        public Guid TenantId { get; }

        public DateTimeOffset CreatedAt { get; }

        public DateTimeOffset UpdatedAt { get; }
    }
}
