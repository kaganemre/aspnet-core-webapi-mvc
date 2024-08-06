using FluentValidation;
using GuitarShopApp.Application.DTO;

namespace GuitarShopApp.Application.Features.Commands;

public class LoginUserValidator : AbstractValidator<LoginDTO>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("'Email' address is required")
            .EmailAddress().WithMessage("A valid email is required");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("The 'Password' field is required.");
    }
}