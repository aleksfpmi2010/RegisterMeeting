using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace MeetingRegistry.EmailService
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string email)
        {
            try
            {
                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("admin", _configuration["SenderEmail"]));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = _configuration["EmailSubject"];
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = _configuration["ValidationLink"]
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration["SenderProvider"], Int32.Parse(_configuration["ProviderPort"]), false);
                    await client.AuthenticateAsync(_configuration["SenderEmail"], _configuration["SenderLogin"]);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
