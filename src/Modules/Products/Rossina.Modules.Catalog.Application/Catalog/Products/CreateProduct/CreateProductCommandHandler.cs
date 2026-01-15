using MediatR;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Title, request.Article, request.Title);

        productRepository.Insert(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}