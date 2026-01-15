using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Products;
using Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

namespace Rossina.Modules.Products.Presentation.Catalog.Products;

internal static class GetProduct
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductQuery(id);
            var product = await sender.Send(query);

            return product is null ? Results.NotFound() : Results.Ok(product);
        }).WithTags(Tags.Product);
    }
}