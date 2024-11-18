using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.MailKit
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await SendEmailAsync(to, subject, body, null, null, null);
        }

        public async Task SendEmailAsync(string to, string subject, string body, string cc)
        {
            await SendEmailAsync(to, subject, body, cc, null, null);
        }

        public async Task SendEmailAsync(string to, string subject, string body, string cc, string bcc)
        {
            await SendEmailAsync(to, subject, body, cc, bcc, null);
        }

        public async Task SendEmailAsync(string to, string subject, string body, List<string> attachments)
        {
            await SendEmailAsync(to, subject, body, null, null, attachments);
        }

        public async Task SendEmailAsync(string to, string subject, string body, string cc, string bcc, List<string> attachments)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.Username),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    mail.Attachments.Add(new Attachment(attachment));
                }
            }

            using var smtp = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
