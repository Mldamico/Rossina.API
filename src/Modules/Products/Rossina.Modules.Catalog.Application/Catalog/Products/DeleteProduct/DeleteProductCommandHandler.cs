using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.DeleteProduct;

internal class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if (product is null)
        {
            return Result.Failure<bool>(ProductsError.NotFound(command.ProductId));
        }
        
        product.Delete();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(true);
        
    }
}