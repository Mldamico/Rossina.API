using System.Data.Common;
using Dapper;
using MediatR;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Catalog.Brands.GetBrand;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IBrandRepository brandRepository)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        
        var brand = await brandRepository.FindBrandAsync(request.BrandId, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Guid>(BrandsErrors.NotFound(request.BrandId));
        }
        
        var result = Product.Create(request.Title, request.Article, request.Title, brand);

        productRepository.Insert(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result.Value.Id;
    }

   
}

