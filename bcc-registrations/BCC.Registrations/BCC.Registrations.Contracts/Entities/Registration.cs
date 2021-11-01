using System;
using System.ComponentModel.DataAnnotations;
using BCC.EntityApi;

namespace BCC.Registrations.Contracts.Entities
{
    public class Registration : BaseEntity
    {
        /* Relationships */
        [Required]
        public Guid ActivityId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        /* Attributes */
        [Required]
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Created;

    }

    public enum RegistrationStatus
    {
        Revoked = 0b_0000, // 0
        Created = 0b_0001, // 1
        CheckedIn = 0b_0010, // 2
        CheckedOut = 0b_0011, // 3

        Pending = 0b_0100, // 4
        PendingPayment = 0b_0101, // 5
        PendingApproval = 0b_0110, // 6

        Confirmed = 0b_1111, // 15
    }
}
