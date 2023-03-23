using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LMS_API_BusinessLayer.Messaging
{

    public class EmailMessageSender : IMessageSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;

        public EmailMessageSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string fromEmail)
        {

            _fromEmail = fromEmail;

            _smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MailMessage(_fromEmail, to, subject, body)
            {
                IsBodyHtml = false
            };

            await _smtpClient.SendMailAsync(message);
        }
    }

}
