using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Brands.CreateBrand;

namespace Rossina.Modules.Products.Presentation.Catalog.Brands;

internal static class CreateBrand
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/brands", async (CreateBrandRequest request, ISender sender) =>
        {
            var command = new CreateBrandCommand(request.Name, request.Logo);
            var result = await sender.Send(command);

            return Results.Ok(result);
        }).WithTags(Tags.Brands);
    }
    
}

internal sealed class CreateBrandRequest
{
    public string Name { get; set; }
    public string Logo { get; set; }
}