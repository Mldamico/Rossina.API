using Rossina.Common.Application.Messaging;
using Rossina.Common.Domain.Abstractions;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.DeleteBrand;

internal sealed class DeleteBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.FindBrandAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return  Result.Failure<Guid>(BrandsErrors.NotFound(request.Id));
        }
        
        brand.Delete();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(brand.Id);
    }
}