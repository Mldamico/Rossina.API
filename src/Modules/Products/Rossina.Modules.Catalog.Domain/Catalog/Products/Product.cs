using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog;

public sealed class Product : Entity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Article { get; private set; }
    public string Description { get; private set; }
    public Guid BrandId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public ProductStatus Status { get; private set; } = ProductStatus.Default;

    private readonly List<ProductVariant> _variants = new();
    public ICollection<ProductVariant> VariantsInternal => _variants;


    public Product()
    {
    }

    public static Result<Product> Create(string title, string article, string description, Brand brand)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = title,
            Article = article,
            Description = description,
            BrandId =  brand.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));
        
        return Result.Success(product);
    }

    public Result AddVariant(string size,
        string color,
        decimal price,
        int stock)
    {
        var result = ProductVariant.Create(size, color, price, stock, this);

        if (result.IsFailure)
            return result;
        
        _variants.Add(result.Value);
        
        UpdatedAt = DateTime.UtcNow;

        return Result.Success();

    }
    
}

public sealed class ProductCreatedDomainEvent(Guid productId) : DomainEvent
{
    public Guid Id { get; } = productId;
}