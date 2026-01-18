using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Products;

internal static class CreateProduct
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (Request request, ISender sender) =>
            {
                var command = new CreateProductCommand(
                    request.Title,
                    request.Article,
                    request.Description,
                    request.BrandId
                );

                var result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
            })
            .WithTags(Tags.Product);
    }
}

internal sealed class Request
{
    public string Title { get; set; }
    public string Article { get; set; }
    public string Description { get; set; }
    public Guid BrandId { get; set; }
}