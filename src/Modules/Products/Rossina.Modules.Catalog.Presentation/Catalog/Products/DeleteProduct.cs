using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Products.DeleteProduct;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Products;

internal static class DeleteProduct
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/products/{productId}/delete", async (Guid productId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(productId));
            
            result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        });
    }
}