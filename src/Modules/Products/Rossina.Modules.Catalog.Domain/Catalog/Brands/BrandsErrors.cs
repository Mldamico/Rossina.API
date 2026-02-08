
using Rossina.Common.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog.Brands;

public static class BrandsErrors
{
    public static Error NotFound(Guid brandId) =>
        Error.NotFound("Brands.NotFound",$"The brand with ID {brandId} was not found.");
}