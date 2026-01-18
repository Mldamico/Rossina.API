using FluentValidation;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.AddVariant;

public sealed class AddVariantCommandValidator : AbstractValidator<AddVariantCommand>
{
    public AddVariantCommandValidator()
    {
        RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required.");
        RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required.");
        RuleFor(x=> x.Stock).Must(stock => stock >= 0).WithMessage("Stock must be equal or greater than zero.");
        RuleFor(x => x.Price).Must(price => price > 0).WithMessage("Price must be greater than zero.");
        
    }
}