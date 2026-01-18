using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.AddVariant;

public sealed record AddVariantResponse(bool Success);

public class AddVariantCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : ICommandHandler<AddVariantCommand, AddVariantResponse>
{
    public async Task<Result<AddVariantResponse>> Handle(AddVariantCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if (product is null)
        {
            return Result.Failure<AddVariantResponse>(ProductsError.NotFound(command.ProductId));
        }
        
        var result = product.AddVariant(
            command.Size,
            command.Color,
            command.Price,
            command.Stock);

        if(result.IsFailure)
            return Result.Failure<AddVariantResponse>(ProductsError.VariantError(command.ProductId));
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(new AddVariantResponse(true));
    }
}