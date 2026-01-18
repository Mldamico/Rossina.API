using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Brands.CreateBrand;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Brands;

internal static class CreateBrand
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/brands", async (CreateBrandRequest request, ISender sender) =>
        {
            var command = new CreateBrandCommand(request.Name, request.Logo);
            var result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        }).WithTags(Tags.Brands);
    }
    
}

internal sealed class CreateBrandRequest
{
    public string Name { get; set; } = default!;
    public string Logo { get; set; } = default!;
}