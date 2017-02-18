using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
namespace Sample.AspCoreEF.Common
{
    public class MailHelper
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {

            var host = CommonConstants.SMTPHost;
            var port = int.Parse(CommonConstants.SMTPPort);
            var fromEmail = CommonConstants.FromEmailAddress;
            var password = CommonConstants.FromEmailPassword;
            var fromName = CommonConstants.FromName;

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(fromName, fromEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(host, port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.     client.Authenticate("anuraj.p@example.com", "password");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            
        }
    }
}
