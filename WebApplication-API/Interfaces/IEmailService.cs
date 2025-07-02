namespace WebApplication_API.Interfaces
{
    public interface IEmailService
    {
        Task SendInvoiceEmailAsync(string toEmail, string subject, string htmlContent);
    }
}
