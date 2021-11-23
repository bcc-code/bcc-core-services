using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Core.Api.Services
{
    public class MailMsFlowClient : IMailMsFlowClient
    {
        private readonly IConfiguration _config;
        private readonly string _msFlowEmailUrl = "";

        public MailMsFlowClient(IConfiguration config)
        {
            _config = config;
            _msFlowEmailUrl = _config.GetValue<string>("MsFlowEmailUrl");
        }

        public async Task<bool> Send(string subject, string body, bool isHtml, MailAddress from,
            MailAddressCollection to, MailAddressCollection replyTo, MailAddressCollection bcc = null)
        {
            if (isHtml) body = ConvertToHtml(body);


            var message = new MsFlowEmailData
            {
                Body = body,
                From = from.Address,
                Subject = subject,
                To = to.Select(s => s.Address).ToList()
            };
            if (replyTo != null) message.ReplyTo = replyTo.Select(s => s.Address).ToList();

            return await SendToMsFlow(message);
        }

        private string ConvertToHtml(string body)
        {
            return
                "<html><head><style type=\"text/css\">body{font: 12px Arial, Helvetica, sans-serif; width:100%;} h2{font-size:16px; font-weight:normal; margin: 15px 0 5px 0;} table{margin-bottom:5px;}</style></head><body>" +
                body + "</body></html>";
        }

        private async Task<bool> SendToMsFlow(MsFlowEmailData message)
        {
            var json = JsonConvert.SerializeObject(message);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(_msFlowEmailUrl, data);
                return response?.StatusCode == HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                // _log.TrackException(ex);
                Console.WriteLine("Exception:" + ex);
                return false;
            }
        }
    }

    public interface IMailMsFlowClient
    {
        public Task<bool> Send(string subject, string body, bool isHtml, MailAddress from, MailAddressCollection to,
            MailAddressCollection replyTo, MailAddressCollection bcc = null);
    }

    public class MsFlowEmailData
    {
        public string Body { get; set; }
        public string From { get; set; }
        public IList<string> To { get; set; }
        public string Subject { get; set; }
        public IList<string> ReplyTo { get; set; }
    }
}