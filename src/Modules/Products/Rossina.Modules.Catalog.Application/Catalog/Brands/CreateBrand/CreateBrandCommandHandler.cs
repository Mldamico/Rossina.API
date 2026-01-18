using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.CreateBrand;

internal sealed class CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        var result = Brand.Create(command.Name, command.Logo);
        
        brandRepository.Insert(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(result.Value.Id);
    }
}