using System;
using System.Collections.Generic;
using System.Text;

namespace Bcc.Registrations
{
    public class ApiClientOptions
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
        public string TokenEndpoint { get; set; } = "/oauth/token";

        public string Audience { get; set; }

        public string Scope { get; set; }

        public string ApiBasePath { get; set; }
    }
}
