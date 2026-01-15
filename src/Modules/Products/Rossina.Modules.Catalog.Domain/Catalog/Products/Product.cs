using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog;

public sealed class Product : Entity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Article { get; private set; }
    public string Description { get; private set; }
    public Brand Brand { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public ProductStatus Status { get; private set; } = ProductStatus.Default;
    public ICollection<ProductVariant> Variants { get; private set; }

    public Product()
    {
    }

    public static Product Create(string title, string article, string description)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = title,
            Article = article,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));
        
        return product;
    }
}

public sealed class ProductCreatedDomainEvent(Guid productId) : DomainEvent
{
    public Guid Id { get; } = productId;
}