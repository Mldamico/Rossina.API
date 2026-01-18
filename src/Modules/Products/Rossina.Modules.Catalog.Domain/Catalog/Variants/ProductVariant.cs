using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog;

public sealed class ProductVariant : Entity
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Size { get; private set; }
    public string Color { get;  private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public bool IsActive { get; private set; } = true;
    
    public ProductVariant()
    {
    }

    public static Result<ProductVariant> Create(string size, string color, decimal price, int stock, Product product)
    {
        var productVariant = new ProductVariant
        {
            Id = Guid.NewGuid(),
            Size = size,
            Color = color,
            Price = price,
            Stock = stock,
            ProductId = product.Id
        };
        
        return Result.Success(productVariant);
    }
}