using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using GodfatherTips.Utilities;

namespace GodfatherTips.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.ApiUser, Options.ApiKey, Options.SmtpServer, Options.PortNumber, subject, message, email);
        }

        public async Task Execute(string apiUser, string apiKey, string smtpServer, int portNumber, string subject, string message, string email)
        {
            using (var smtp = new SmtpClient
            {
                Host = smtpServer,
                Port = portNumber,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(apiUser, apiKey)
            })
            {
                using (var msg = new MailMessage(apiUser, email))
                {
                    msg.Subject = subject;
                    msg.Body = message;
                    await smtp.SendMailAsync(msg);
                }
            }
        }
    }
}