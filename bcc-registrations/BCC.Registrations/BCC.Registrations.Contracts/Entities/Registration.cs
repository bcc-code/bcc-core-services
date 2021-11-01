using System;
using System.ComponentModel.DataAnnotations;
using BCC.EntityApi;
using BCC.Registrations.Contracts.Types;

namespace BCC.Registrations.Contracts.Entities
{
    public class Registration : BaseEntity
    {
        /* Relationships */
        [Required]
        public Guid ActivityId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid TenantId { get; set; }

        /* Attributes */
        [Required]
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Created;


        public Registration AwaitPendingAction(string ActionDefinition)
        {
            EntityLogExtension.Log(this, new object[]
            {
                Status
            }, ActionDefinition);

            Status = Status >= RegistrationStatus.Pending ? Status + 1 : RegistrationStatus.Pending;

            return this;
        }

        public Registration CompletePendingAction(string ActionCompletedDefinition, bool AutoConfirm = true)
        {
            if (Status < RegistrationStatus.Pending)
            {
                throw new Exception("Registration is not currently pending an action to be completed");
            }

            if (Status == RegistrationStatus.Pending && AutoConfirm)
            {
                return Confirm();
            }

            EntityLogExtension.Log(this, new object[]
            {
                Status
            }, ActionCompletedDefinition);

            Status = Status - 1;

            return this;
        }

        public Registration Confirm()
        {
            if (Status == RegistrationStatus.Confirmed)
            {
                return this;
            }
            if (Status >= RegistrationStatus.Pending)
            {
                throw new Exception("Registration is currently waiting on an action to complete");
            }

            EntityLogExtension.Log(this, new object[]
            {
                Status
            }, "Confirming Registration");

            Status = RegistrationStatus.Confirmed;

            return this;
        }
    }
}
