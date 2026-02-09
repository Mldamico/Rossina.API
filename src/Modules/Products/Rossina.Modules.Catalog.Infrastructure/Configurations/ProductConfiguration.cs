using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Products;
using Rossina.Modules.Catalog.Domain.Catalog.Variants;

namespace Rossina.Modules.Products.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        
        builder.Property(p => p.UpdatedAt)
            .IsConcurrencyToken(false); 
        
        // builder.HasMany(p => p.Variants)
        //     .WithOne(v => v.Product);
        
        builder.Navigation(p => p.Variants)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();
    }
}