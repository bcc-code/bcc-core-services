using System;

namespace Bcc.WebHooks.Receivers.Members.Contracts
{
    public class Member
    {
        public int PersonId { get; set; }
        public string Email { get; set; }
        public DateTime LastChangedDate { get; set; }
    }
}