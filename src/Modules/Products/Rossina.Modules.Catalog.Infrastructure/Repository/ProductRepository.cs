using Microsoft.EntityFrameworkCore;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Products;
using Rossina.Modules.Catalog.Domain.Catalog.Variants;
using Rossina.Modules.Products.Infrastructure.Database;

namespace Rossina.Modules.Products.Infrastructure.Repository;

internal sealed class ProductRepository(ProductsDbContext context) : IProductRepository
{
    public void Insert(Product product)
    {
        context.Products.Add(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
    }
    
    public void Update(Product product)
    {
        var entry = context.Entry(product);
    
        if (entry.State == EntityState.Detached)
        {
            context.Products.Attach(product);
            entry.State = EntityState.Modified;
        }
    }

    public async Task<ProductVariant?> GetProductVariant(Guid productId, string size, string color)
    {
        return await context.ProductVariants.FirstOrDefaultAsync(x => x.ProductId == productId 
            && x.Size == size && x.Color == color);
    }
}