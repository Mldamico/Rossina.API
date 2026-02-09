using FluentValidation;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}