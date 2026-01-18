using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog.Products;

public static class ProductsError
{
    public static Error NotFound(Guid productId) =>
        Error.NotFound("Products.NotFound",$"The product with ID {productId} was not found.");
    
}