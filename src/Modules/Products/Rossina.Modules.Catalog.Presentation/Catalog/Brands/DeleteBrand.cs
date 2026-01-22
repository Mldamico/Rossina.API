using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Brands.DeleteBrand;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Brands;

internal static class DeleteBrand
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/brand/{brandId}/delete", async (Guid brandId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBrandCommand(brandId));
            
            result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        });
    } 
}