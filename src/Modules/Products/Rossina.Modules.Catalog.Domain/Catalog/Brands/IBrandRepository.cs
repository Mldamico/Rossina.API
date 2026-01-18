namespace Rossina.Modules.Catalog.Domain.Catalog.Brands;

public interface IBrandRepository
{
    Task<Brand?> FindBrandAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Brand brand);
}