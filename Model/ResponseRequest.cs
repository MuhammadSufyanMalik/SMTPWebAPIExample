namespace SMTPWebAPIExample.Models
{
    public class EmailRequest
    {
        public Guid MessageId { get; set; }
        public string From { get; set; }
        public List<string> ToList { get; set; }
        public List<string> CcList { get; set; } = new List<string>();

        public List<string> BccList { get; set; } = new List<string>();

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
        public EmailRequest()
        {
        }
        public EmailRequest(Guid messageId, List<string> toList, string from, string subject, string body)
        {
            MessageId = messageId;
            From = from;
            Subject = subject;
            Body = body;
            ToList = toList;
        }

        public EmailRequest(Guid messageId, string to, string from, string subject, string body)
        {
            MessageId = messageId;
            From = from;
            Subject = subject;
            Body = body;
            ToList = new List<string> { to };
        }

        public EmailRequest(Guid messageId, string to, string from, string subject, string body, EmailAttachment attachment)
        {
            MessageId = messageId;
            From = from;
            Subject = subject;
            Body = body;
            ToList = new List<string> { to };
            Attachments = new List<EmailAttachment> { attachment };
        }

        public EmailRequest(Guid messageId, string to, string from, string subject, string body, List<EmailAttachment> attachments)
        {
            MessageId = messageId;
            From = from;
            Subject = subject;
            Body = body;
            ToList = new List<string> { to };
            Attachments = attachments;
        }

    }
    public class EmailAttachment
    {
        public string FileName { get; set; }

        public byte[] Content { get; set; }
    }
}
