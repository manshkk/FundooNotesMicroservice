using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Email;

namespace UserService.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendWelcomeEmailAsync(string email, string firstName)
    {
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        var senderPassword = _configuration["EmailSettings:Password"];
        var smtpServer = _configuration["EmailSettings:SmtpServer"];
        var portText = _configuration["EmailSettings:Port"];

        if (string.IsNullOrWhiteSpace(senderEmail) ||
            string.IsNullOrWhiteSpace(senderPassword) ||
            string.IsNullOrWhiteSpace(smtpServer) ||
            string.IsNullOrWhiteSpace(portText))
        {
            _logger.LogWarning(
                "Email settings are not configured. Welcome email was not sent to {Email}.",
                email);
            return;
        }

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Fundoo Notes", senderEmail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Welcome to Fundoo Notes";
        message.Body = new TextPart("html")
        {
            Text = WelcomeEmailTemplate.Build(firstName)
        };

        try
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                smtpServer,
                int.Parse(portText),
                MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(senderEmail, senderPassword);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Welcome email sent successfully to {Email}.", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send welcome email to {Email}.", email);
        }
    }
}
