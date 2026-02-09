using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rossina.Modules.Catalog.Application.Catalog.Brands.GetBrand;
using Rossina.Modules.Products.Presentation.ApiResults;

namespace Rossina.Modules.Products.Presentation.Catalog.Brands;

internal static class GetBrand
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/brands/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetBrandQuery(id));
            
            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        }).WithTags(Tags.Brands);
    }
}