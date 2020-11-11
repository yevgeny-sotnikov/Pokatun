using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Pokatun.API.Helpers;

namespace Pokatun.API.Services
{
    public class EmailApiService : IEmailApiService
    {
        private readonly MailSettings _mailSettings;

        public EmailApiService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void Send(string to, string subject, string text, string from = null)
        {
            // create message
            from = from ?? _mailSettings.EmailFrom;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = text };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(from, _mailSettings.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
