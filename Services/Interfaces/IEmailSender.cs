namespace InventoryManager.Services.Interfaces;

public interface IInventoryEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    Task SendEmailAsync(string email, string subject, string htmlMessage, string textMessage);
}
