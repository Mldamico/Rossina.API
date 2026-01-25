using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;

public record CreateProductResponse(Guid Id, string Title, string Article, string Description, Guid BrandId, DateTime CreatedAt, DateTime UpdatedAt, ProductStatus Status );

internal sealed class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IBrandRepository brandRepository)
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        
        var brand = await brandRepository.FindBrandAsync(request.BrandId, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<CreateProductResponse>(BrandsErrors.NotFound(request.BrandId));
        }
        
        var result = Product.Create(request.Title, request.Article, request.Title, brand);

        productRepository.Insert(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateProductResponse(result.Value.Id,
            result.Value.Title,
            result.Value.Article,
            result.Value.Description,
            result.Value.BrandId,
            result.Value.CreatedAt,
            result.Value.UpdatedAt,
            result.Value.Status));
    }

   
}

