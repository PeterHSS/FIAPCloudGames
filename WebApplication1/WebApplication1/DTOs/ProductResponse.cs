namespace WebApplication1.DTOs;

public record ProductResponse(Guid Id, string Name, string Description, decimal Price, int StockQuantity, bool IsActive, CategoryResponse Category);
