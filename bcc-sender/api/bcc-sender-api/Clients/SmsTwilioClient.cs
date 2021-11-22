using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace bcc_sender_api.Clients
{
    public class SmsTwilioClient : ISmsTwilioClient
    {
        //Test account data
        private const string AccountSid = "AC97baf19bf67d3c250dae130f17af5980";
        private const string AuthToken = "";
        private const string MessagingServiceSid = "MG15ddca5734d8ca7e1175f815889c72ff";

        public async Task<MessageResource> SendSms(string number, string body)
        {
            TwilioClient.Init(AccountSid, AuthToken); 
            
            var messageOptions = new CreateMessageOptions( 
                new PhoneNumber(number))
            {
                MessagingServiceSid = MessagingServiceSid,
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