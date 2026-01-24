using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.CreateBrand;

public record CreateBrandResponse(Guid Id, String Name, String Logo);

internal sealed class CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateBrandCommand, CreateBrandResponse>
{
    public async Task<Result<CreateBrandResponse>> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        var result = Brand.Create(command.Name, command.Logo);
        
        brandRepository.Insert(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(new CreateBrandResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.Logo
        ));
    }
}