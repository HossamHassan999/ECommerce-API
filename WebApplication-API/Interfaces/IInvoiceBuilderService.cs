using WebApplication_API.Data;

namespace WebApplication_API.Interfaces
{
    public interface IInvoiceBuilderService
    {
        string BuildInvoiceHtml(Order order, List<OrderItem> items, User user);
    }
}
