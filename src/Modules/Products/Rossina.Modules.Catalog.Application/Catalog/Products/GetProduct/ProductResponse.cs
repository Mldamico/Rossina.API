namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

public sealed record ProductResponse(
    Guid Id,
    string Title,
    string Article,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt);