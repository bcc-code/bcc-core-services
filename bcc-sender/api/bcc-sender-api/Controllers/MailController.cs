
using System.Net.Mail;
using System.Threading.Tasks;
using bcc_sender_api.Clients;
using bcc_sender_api.Models;
using Core.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace bcc_sender_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly ILogger<MailController> _logger;
        private readonly IMailMsFlowClient _mailMsFlowClient;

        public MailController(ILogger<MailController> logger, IMailMsFlowClient mailMsFlowClient)
        {
            _logger = logger;
            _mailMsFlowClient = mailMsFlowClient;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MailToSend mailToSend)
        {
            var mailAddress = new MailAddress(mailToSend.EmailAddress);
            var mailAddressCollection = new MailAddressCollection();
            mailAddressCollection.Add(mailAddress);
            var result = await _mailMsFlowClient.Send("Test", mailToSend.Content, false, mailAddress,
                mailAddressCollection, mailAddressCollection, null);
            _logger.Log(LogLevel.Information,"Email message was sent");
            return Ok(result);
        }
    }
}