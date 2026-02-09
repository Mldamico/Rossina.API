using Rossina.Common.Application.Messaging;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.AddVariant;

public sealed record AddVariantCommand(Guid ProductId,
    string Size,
    string Color,
    decimal Price,
    int Stock) : ICommand<AddVariantResponse>;