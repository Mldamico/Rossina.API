
using Rossina.Common.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Domain.Catalog.Variants;

public static class VariantsError
{
    public static Error VariantError(Guid productId) =>
        Error.Failure("Products.VariantError",$"The product with ID {productId} was not found.");

    public static Error VariantAlreadyExists(Guid productId, string size, string color) =>
        Error.Conflict("Products.Variants", $"The variant with ID {productId} for size {size} with {color} already exists.");
}