using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog;

public sealed class Brand : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Logo { get; set; } = default!;

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
}