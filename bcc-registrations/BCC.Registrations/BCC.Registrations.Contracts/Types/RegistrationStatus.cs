using System;
namespace BCC.Registrations.Contracts.Types
{
    [Flags]
    public enum RegistrationStatus
    {
        Revoked = 0b_0000, // 0
        Created = 0b_0001, // 1
        Confirmed = 0b_0010, // 2

        Pending = 0b_1000, // 8>=
    }
}
