using Microsoft.EntityFrameworkCore;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Domain.Catalog;

namespace Rossina.Modules.Products.Infrastructure.Database;

public sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductVariant> ProductVariants { get; set; } = default!;
    public DbSet<Brand> Brands { get; set; } = default!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Products);
        base.OnModelCreating(modelBuilder);
    }
}

internal static class Schemas
{
    public static readonly string Products = "Products";
}

//Referencias
//Aplication -: Domain
//Infrastru -> Presentation y Application
//Presentation -> Application