using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Products.AddVariant;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Products;

public static class AddVariant
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/products/{productId}/variants",
            async (Guid productId, [FromBody] Request request, ISender sender) =>
            {
                var command = new AddVariantCommand(productId, request.Size, request.Color, request.Price, request.Stock);
                var result = await sender.Send(command);
                
                return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
            });
    }
    
    internal sealed class Request
    {
        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
