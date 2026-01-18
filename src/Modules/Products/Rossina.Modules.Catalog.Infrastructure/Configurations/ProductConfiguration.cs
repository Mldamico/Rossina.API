using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossina.Modules.Catalog.Domain.Catalog;

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
        
        builder.HasMany<ProductVariant>("_variants")
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_variants")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();
        
    }
}