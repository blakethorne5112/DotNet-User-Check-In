using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace DotNetAssign2
{
    // EmailSettings.cs
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public bool IsSSL { get; set; }
    }

    // IEmailSender.cs
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    // EmailSender.cs
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_emailSettings.MailServer)
            {
                Port = _emailSettings.MailPort,
                Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password),
                EnableSsl = _emailSettings.IsSSL,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle the exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }

}
