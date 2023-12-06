using SMTPWebAPIExample.Models;

namespace SMTPWebAPIExample.Abstract
{
    public interface IEmailService
    {
        void SendEmailList(EmailRequest requestEmail);
        void SendEmail(EmailRequest requestEmail);
    }
}
