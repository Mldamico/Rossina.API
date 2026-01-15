using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Products;
using Rossina.Modules.Products.Infrastructure.Database;

namespace Rossina.Modules.Products.Infrastructure.Repository;

internal sealed class ProductRepository(ProductsDbContext context) : IProductRepository
{
    public void Insert(Product product)
    {
        context.Products.Add(product);
    }
}