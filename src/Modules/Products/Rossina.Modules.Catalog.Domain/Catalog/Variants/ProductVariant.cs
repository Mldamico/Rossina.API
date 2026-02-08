using Rossina.Common.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Domain.Catalog.Variants;

public sealed class ProductVariant : Entity
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Size { get; private set; }
    public string Color { get;  private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public bool Deleted { get; private set; } = false;
    public Product Product { get; private set; }
    
    public ProductVariant()
    {
    }

    public Result UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
        return Result.Success();
    }

    public Result UpdateStock(int newStock)
    {
        Stock = newStock;
        return Result.Success();
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
    
    public static Result<ProductVariant> Create(Guid id, string size, string color, decimal price, int stock, Guid productId)
    {
        var productVariant = new ProductVariant
        {
            Id = id,
            Size = size,
            Color = color,
            Price = price,
            Stock = stock,
            ProductId = productId
        };
        
        return Result.Success(productVariant);
    }
}