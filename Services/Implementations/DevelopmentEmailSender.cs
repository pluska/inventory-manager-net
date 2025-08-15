using InventoryManager.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryManager.Services.Implementations;

public class DevelopmentEmailSender : IInventoryEmailSender
{
    private readonly ILogger<DevelopmentEmailSender> _logger;

    public DevelopmentEmailSender(ILogger<DevelopmentEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return SendEmailAsync(email, subject, htmlMessage, string.Empty);
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage, string textMessage)
    {
        _logger.LogInformation("=== EMAIL SENT (Development Mode) ===");
        _logger.LogInformation("To: {Email}", email);
        _logger.LogInformation("Subject: {Subject}", subject);
        _logger.LogInformation("HTML Message: {HtmlMessage}", htmlMessage);
        
        if (!string.IsNullOrEmpty(textMessage))
        {
            _logger.LogInformation("Text Message: {TextMessage}", textMessage);
        }
        
        _logger.LogInformation("=== END EMAIL ===");
        
        // In development, we just log the email
        // In production, you would integrate with SendGrid, SMTP, etc.
        return Task.CompletedTask;
    }
}
