using System;
using System.Collections.Generic;
using System.Text;

namespace Bcc.Registrations.Exceptions
{
    public class ApiRequestException : Exception
    {
        public ApiRequestException(string message) : base(message)
        {
        }
    }
}
