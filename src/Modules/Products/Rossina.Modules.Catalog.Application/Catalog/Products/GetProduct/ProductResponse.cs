namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

public sealed record ProductResponse( Guid Id, string Title, string Article, string Description, DateTime CreatedAt, DateTime UpdatedAt, VariantResponse[] Variants );
public sealed record VariantResponse( string Size, string Color, decimal Price, int Stock );

public sealed record ProductVariantRow(
    Guid Id,
    string Title,
    string Article,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Size,
    string Color,
    decimal Price,
    int Stock
);