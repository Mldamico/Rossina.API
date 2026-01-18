using Microsoft.EntityFrameworkCore;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;
using Rossina.Modules.Products.Infrastructure.Database;

namespace Rossina.Modules.Products.Infrastructure.Repository;

public class BrandRepository(ProductsDbContext context) : IBrandRepository
{
    public async Task<Brand?> FindBrandAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Brands.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Insert(Brand brand)
    {
        context.Brands.Add(brand);
    }
}