using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;
using Rossina.Modules.Catalog.Domain.Catalog.Variants;

namespace Rossina.Modules.Products.Infrastructure.Data;

public static class InitialData
{
    public static IEnumerable<Brand> Brands() => new List<Brand>
    {
        Brand.Create(Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff61"), "Selu","hola").Value,
    };

    public static IEnumerable<Product> Products() => new List<Product>
    {
        Product.Create(Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff62"), "Corpiño Soft", "4249","Soutien",Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff61")).Value
    };

    public static IEnumerable<ProductVariant> Variants() => new List<ProductVariant>
    {
        ProductVariant.Create(Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff63"), "85", "Blanco", 10000.0m, 2,Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff62")).Value
    };
}