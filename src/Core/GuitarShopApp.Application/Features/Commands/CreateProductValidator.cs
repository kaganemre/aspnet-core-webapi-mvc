using FluentValidation;
using GuitarShopApp.Application.DTO;

namespace GuitarShopApp.Application.Features.Commands;

public class CreateProductValidator : AbstractValidator<ProductDTO>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("The 'Name' field is required.")
            .MaximumLength(20)
            .MinimumLength(3)
            .WithMessage("'Name' value must be between 3 and 20 characters.");
        RuleFor(p => p.Price)
            .Must(p => p > 0)
            .WithMessage("The 'Price' field must be greater than zero.");
    }
}