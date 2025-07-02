using MimeKit;
using WebApplication_API.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace WebApplication_API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendInvoiceEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = htmlContent
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:Smtp"], Convert.ToInt16((_config["Email:Port"])), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
