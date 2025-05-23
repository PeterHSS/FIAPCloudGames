using WebApplication1.Entities.Abstractions;

namespace WebApplication1.Entities;

public class OrderItem : Entity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}


