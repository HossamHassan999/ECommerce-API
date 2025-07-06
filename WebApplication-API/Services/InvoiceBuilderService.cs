using System.Text;
using WebApplication_API.Data;
using WebApplication_API.Interfaces;

namespace WebApplication_API.Services
{
    public class InvoiceBuilderService : IInvoiceBuilderService
    {
        public string BuildInvoiceHtml(Order order, List<OrderItem> items, User user)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"<h2>Hi {user.Name},</h2>");
            sb.AppendLine($"<p>Thanks for your order #{order.Id}!</p>");
            sb.AppendLine("<h3>Order Details:</h3>");
            sb.AppendLine("<table border='1' cellpadding='5'><tr><th>Product</th><th>Qty</th><th>Price</th></tr>");

            foreach (var item in items)
            {
                sb.AppendLine($"<tr><td>{item.Product.Name}</td><td>{item.Quantity}</td><td>{item.Price:C}</td></tr>");
            }

            var total = items.Sum(i => i.Quantity * i.Price);
            sb.AppendLine($"</table><p><strong>Total: {total:C}</strong></p>");
            sb.AppendLine("<p>We will contact you once your order is shipped.</p>");

            return sb.ToString();
        }
    }
}
