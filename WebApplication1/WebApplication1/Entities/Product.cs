using WebApplication1.Entities.Abstractions;

namespace WebApplication1.Entities;

public class Product : Entity
{
    public string Name { get; set; }            
    public string Description { get; set; }     
    public decimal Price { get; set; }          
    public int StockQuantity { get; set; }      
    public bool IsActive { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}