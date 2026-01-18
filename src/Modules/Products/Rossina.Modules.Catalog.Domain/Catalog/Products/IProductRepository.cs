namespace Rossina.Modules.Catalog.Domain.Catalog.Products;

public interface IProductRepository
{
    void Insert(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    void Update(Product product);
}