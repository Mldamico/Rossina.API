using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand<bool>;