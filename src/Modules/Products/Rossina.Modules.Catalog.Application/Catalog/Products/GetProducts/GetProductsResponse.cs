using Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProducts;

public class GetProductsResponse
(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<ProductResponse> Products);