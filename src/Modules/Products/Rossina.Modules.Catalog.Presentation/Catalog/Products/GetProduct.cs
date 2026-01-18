using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Products;

internal static class GetProduct
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductQuery(id);
            var result = await sender.Send(query);
            
            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        }).WithTags(Tags.Product);
    }
}