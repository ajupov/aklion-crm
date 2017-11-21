using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Business.Mail.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Aklion.Crm.Business.Mail
{
    public class MailService : IMailService
    {
        private readonly MailServiceConfiguration _configuration;

        public MailService(IOptions<MailServiceConfiguration> options)
        {
            _configuration = options.Value;
        }

        public Task Send(string from, string to, string subject, string message)
        {
            var mimeMessage = new MimeMessage
            {
                From = {new MailboxAddress(string.Empty, from)},
                To = {new MailboxAddress(string.Empty, to)},
                Subject = subject,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = message
                }
            };

            return Send(new[] {mimeMessage});
        }

        public Task SendFromAdmin(string to, string subject, string message)
        {
            var mimeMessage = new MimeMessage
            {
                From = {new MailboxAddress("Админстратор", _configuration.AccountName)},
                To = {new MailboxAddress(string.Empty, to)},
                Subject = subject,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = message
                }
            };

            return Send(new[] {mimeMessage});
        }

        private async Task Send(IEnumerable<MimeMessage> messages)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration.SmtpHost, _configuration.SmtpPort, true)
                        .ConfigureAwait(false);

                    await client.AuthenticateAsync(_configuration.AccountName, _configuration.Password)
                        .ConfigureAwait(false);

                    foreach (var message in messages)
                        await client.SendAsync(message).ConfigureAwait(false);

                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}