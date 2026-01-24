using MediatR;
using Rossina.Modules.Catalog.Application.Messaging;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;

public sealed record CreateProductCommand(string Title, string Article, string Description, Guid BrandId) : ICommand<CreateProductResponse>;