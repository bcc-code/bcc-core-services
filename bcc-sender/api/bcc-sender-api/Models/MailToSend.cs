namespace bcc_sender_api.Models
{
    public class MailToSend
    {
        public string ToEmailAddress { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }

        public bool IsHtml { get; set; }

        public string FromEmailAddress { get; set; }

        public string[] BccEmailsAddresses { get; set; }

        public string[] ReplyToEmailsAddresses { get; set; }
    }
}