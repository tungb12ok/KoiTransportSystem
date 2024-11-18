using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.MailKit
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendEmailAsync(string to, string subject, string body, string cc);
        Task SendEmailAsync(string to, string subject, string body, string cc, string bcc);
        Task SendEmailAsync(string to, string subject, string body, List<string> attachments);
    }

}
