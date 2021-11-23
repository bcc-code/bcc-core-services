﻿using System.Threading.Tasks;
using bcc_sender_api.Clients;
using bcc_sender_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bcc_sender_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ILogger<SmsController> _logger;
        private readonly ISmsTwilioClient _smsClient;

        public SmsController(ILogger<SmsController> logger, ISmsTwilioClient smsClient)
        {
            _logger = logger;
            _smsClient = smsClient;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SmsToSend smsToSend)
        {
            var result = await _smsClient.SendSms(smsToSend.Number, smsToSend.Content);
            _logger.Log(LogLevel.Information, "Sms message was sent");
            return Ok(result);
        }
    }
}