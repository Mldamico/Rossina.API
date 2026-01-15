using Microsoft.AspNetCore.Routing;

namespace Rossina.Modules.Products.Presentation.Catalog.Products;

public class ProductEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateProduct.MapEndpoint(app);
        GetProduct.MapEndpoint(app);
    }
}