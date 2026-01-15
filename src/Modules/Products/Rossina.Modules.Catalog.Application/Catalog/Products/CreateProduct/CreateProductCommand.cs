using MediatR;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;

public sealed record CreateProductCommand(string Title, string Article, string Description) : IRequest<Guid>;