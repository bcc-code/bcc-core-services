using System;
using System.Net.Mail;
using System.Threading.Tasks;
using bcc_sender_api.Models;
using Core.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bcc_sender_api.Controllers
{
    [ApiController]
    [Authorize]
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
            try
            {
                var replyToCollection = new MailAddressCollection();
                foreach (var address in mailToSend.ReplyToEmailsAddresses) replyToCollection.Add(address);

                var bccCollection = new MailAddressCollection();
                foreach (var address in mailToSend.BccEmailsAddresses) bccCollection.Add(address);


                var result = await _mailMsFlowClient.Send(
                    subject: mailToSend.Subject,
                    body: mailToSend.Content,
                    isHtml: mailToSend.IsHtml,
                    to: new MailAddressCollection { mailToSend.ToEmailAddress },
                    from: new MailAddress(mailToSend.FromEmailAddress),
                    replyTo: replyToCollection,
                    bcc: bccCollection
                );
                _logger.Log(LogLevel.Information, "Email message was sent");
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }

         
        }
    }
}