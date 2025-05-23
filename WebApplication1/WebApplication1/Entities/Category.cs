using WebApplication1.Entities.Abstractions;

namespace WebApplication1.Entities;

public class Category : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}