using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog.Brands;

public sealed class Brand : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Logo { get; set; } = default!;
    public bool Deleted { get; set; } = false;

    public static Result<Brand> Create(string name, string logo)
    {
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = name,
            Logo = logo
        };
        
        return Result.Success(brand);
    }
    
    public Result<bool> Delete()
    {
        Deleted = true;

        return Result.Success(true);
    }
}