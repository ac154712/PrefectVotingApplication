using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public SmtpEmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtpHost = _config["Smtp:Host"];
        var smtpPort = int.Parse(_config["Smtp:Port"]);
        var smtpUser = _config["Smtp:Username"];
        var smtpPass = _config["Smtp:Password"];
        var from = _config["Smtp:From"];

        using (var client = new SmtpClient(smtpHost, smtpPort))
        {
            client.Host = smtpHost ;
            client.Port = smtpPort ;
            client.Credentials = new NetworkCredential(smtpUser, smtpPass);
            client.EnableSsl = true; // needed for outlook so it can connect securely with outlook

            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
