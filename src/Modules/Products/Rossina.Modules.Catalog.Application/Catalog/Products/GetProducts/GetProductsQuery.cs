using Rossina.Common.Application.Messaging;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProducts;

public sealed record GetProductsQuery(String Title, String Article, string Brand, int Page, int PageSize) 
    : IQuery<GetProductsResponse>;