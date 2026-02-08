using Rossina.Common.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog.Brands;

public sealed class Brand : Entity
{
    public Guid Id { get;  private set; }
    public string Name { get; private set; } = default!;
    public string Logo { get; private set; } = default!;
    public bool Deleted { get; private set; } = false;

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
    
    public static Result<Brand> Create(Guid id, string name, string logo)
    {
        var brand = new Brand
        {
            Id = id,
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