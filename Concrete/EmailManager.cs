using Microsoft.Extensions.Options;
using SMTPWebAPIExample.Abstract;
using SMTPWebAPIExample.Models;
using System.Net;
using System.Net.Mail;

namespace SMTPWebAPIExample.Concrete

{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly List<EmailSettings> _emailSettings;
        private readonly EmailOptions _emailOptions;
        public EmailManager(IConfiguration configuration, IOptions<List<EmailSettings>> emailSettings, IOptions<EmailOptions> emailOptions)
        {
            _configuration = configuration;
            _emailSettings = emailSettings.Value;
            _emailOptions = emailOptions.Value;
        }

        public void SendEmail(EmailRequest requestEmail)
        {

            using var smtpClient = new SmtpClient(_emailOptions.Host, _emailOptions.Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = _emailOptions.EnableSsl;
            smtpClient.Credentials = new NetworkCredential(_emailOptions.Email, _emailOptions.Password);

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailOptions.Email, _emailOptions.DisplayName)
            };
            foreach (var to in requestEmail.ToList)
            {
                mailMessage.To.Add(to);
            }
            foreach (var cc in requestEmail.CcList)
            {
                mailMessage.CC.Add(cc);
            }
            foreach (var bcc in requestEmail.BccList)
            {
                mailMessage.Bcc.Add(bcc);
            }
            foreach (var attachment in requestEmail.Attachments)
            {
                mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachment.Content), attachment.FileName));
            }
            mailMessage.Headers.Add("Message-Id", requestEmail.MessageId.ToString());
            mailMessage.Headers.Add("Disposition-Notification-To", "malikmsufyan@gmail.com");
            mailMessage.Subject = requestEmail.Subject;
            mailMessage.Body = requestEmail.Body;
            mailMessage.IsBodyHtml = true;


            try
            {
                //  smtpClient.Send(mailMessage);

                smtpClient.SendMailAsync(mailMessage).GetAwaiter().GetResult();
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }

        public void SendEmailList(EmailRequest requestEmail)
        {
            //    string smtpHost = _configuration["SmtpSettings:Host"];
            //    int smtpPort = _configuration.GetValue<int>("SmtpSettings:Port");
            //    string smtpUsername = _configuration["SmtpSettings:Username"];
            //    string smtpPassword = _configuration["SmtpSettings:Password"];
            //    bool enableSsl = _configuration.GetValue<bool>("SmtpSettings:EnableSsl");
            var emailSettings = _emailSettings.FirstOrDefault(x => x.Email == requestEmail.From);
            using var smtpClient = new SmtpClient(emailSettings.Host, emailSettings.Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailSettings.Email, emailSettings.Password);
            smtpClient.EnableSsl = emailSettings.EnableSsl;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.Email, emailSettings.DisplayName)
            };
            mailMessage.To.Add(requestEmail.ToList[0]);
            mailMessage.Subject = requestEmail.Subject;
            mailMessage.Body = requestEmail.Body;
            mailMessage.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
