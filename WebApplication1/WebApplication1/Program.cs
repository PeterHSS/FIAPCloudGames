using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapPost("/api/categories", async (List<CategoryCreateRequest> request, AppDbContext db) =>
{
    var categories = request.Select(request => new Category
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Description = request.Description,
        IsActive = true,
        CreatedAt = DateTime.UtcNow
    });

    db.Categories.AddRange(categories);

    await db.SaveChangesAsync();

    return Results.Created();
});

app.MapPost("/api/products", async (ProductCreateRequest request, AppDbContext db) =>
{
    var products = new Product()
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Description = request.Description,
        Price = request.Price,
        StockQuantity = request.StockQuantity,
        IsActive = true,
        CategoryId = request.CategoryId,
        CreatedAt = DateTime.UtcNow
    };

    db.Products.Add(products);

    await db.SaveChangesAsync();

    return Results.Created();
});

app.MapGet("/api/products/{id}/category", async (Guid categoryId, AppDbContext db) =>
{
    var products = await db.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();

    var response = products.Select(product => new ProductResponse(
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.StockQuantity,
        product.IsActive,
        new CategoryResponse(product.Category.Name, product.Category.Description)));

    return Results.Ok(response);
});

app.MapGet("/api/products/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var product = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

    if (product is null)
    {
        return Results.NotFound();
    }

    var response = new ProductResponse(
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.StockQuantity,
        product.IsActive,
        new CategoryResponse(product.Category.Name, product.Category.Description));

    return Results.Ok(response);
});

app.UseHttpsRedirection();

app.Run();