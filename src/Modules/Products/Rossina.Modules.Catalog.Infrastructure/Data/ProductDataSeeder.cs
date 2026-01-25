using Microsoft.EntityFrameworkCore;
using Rossina.Modules.Products.Infrastructure.Database;

namespace Rossina.Modules.Products.Infrastructure.Data;

public class ProductDataSeeder(ProductsDbContext context) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!await context.Brands.AnyAsync())
        {
            await context.Brands.AddRangeAsync(InitialData.Brands());
           
            await context.SaveChangesAsync();
        }

        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products());
            await context.SaveChangesAsync();
        }

        if (!await context.ProductVariants.AnyAsync())
        {
            await context.ProductVariants.AddRangeAsync(InitialData.Variants());
            await context.SaveChangesAsync();
        }
    }
}