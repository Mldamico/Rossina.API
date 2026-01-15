using FluentValidation;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Article).NotNull().NotEmpty();
        RuleFor(y => y.Title).NotNull().NotEmpty();        
    }
}