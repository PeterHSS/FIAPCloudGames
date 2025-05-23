namespace WebApplication1.DTOs;

internal record ProductCreateRequest(string Name, string Description, decimal Price, int StockQuantity, Guid CategoryId);
