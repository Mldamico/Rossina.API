using Microsoft.AspNetCore.Routing;

namespace Rossina.Modules.Products.Presentation.Catalog.Brands;

public static class BrandEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateBrand.MapEndpoints(app);
    }
}