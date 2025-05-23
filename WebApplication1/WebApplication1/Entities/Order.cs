using WebApplication1.Entities.Abstractions;

namespace WebApplication1.Entities;

public class Order : Entity
{
    public DateTime OrderedAt { get; set; }

    public ICollection<OrderItem> Items { get; set; } = [];
}


