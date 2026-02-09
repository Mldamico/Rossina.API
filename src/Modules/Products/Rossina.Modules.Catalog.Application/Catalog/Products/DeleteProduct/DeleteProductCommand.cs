using Rossina.Common.Application.Messaging;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand<bool>;