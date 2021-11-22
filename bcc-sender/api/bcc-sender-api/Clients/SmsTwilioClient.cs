using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace bcc_sender_api.Clients
{
    public class SmsTwilioClient : ISmsTwilioClient
    {
        private readonly IConfiguration _config;
        private readonly string _accountSid;
        private readonly string _messagingServiceSid;
        private readonly string _authToken;
        public SmsTwilioClient(IConfiguration config)
        {
            _config = config;
            _accountSid = _config.GetValue<string>("Twilio:AccountSid");
            _messagingServiceSid = _config.GetValue<string>("Twilio:MessagingServiceSid");
            _authToken = _config.GetValue<string>("Twilio:AuthToken");
        }

        public async Task<MessageResource> SendSms(string number, string body)
        {
            TwilioClient.Init(_accountSid, _authToken); 
            
            var messageOptions = new CreateMessageOptions( 
                new PhoneNumber(number))
            {
                MessagingServiceSid = _messagingServiceSid,
                Body = body
            };
            return  await MessageResource.CreateAsync(messageOptions); 
        }
       
    }

    public interface ISmsTwilioClient
    {
        public Task<MessageResource> SendSms(string number, string body);
    }
}