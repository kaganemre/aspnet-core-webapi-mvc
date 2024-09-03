using FluentValidation;
using GuitarShopApp.Application.DTO;

namespace GuitarShopApp.Application.Features.Commands;

public class RegisterUserValidator : AbstractValidator<UserDTO>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.FullName)
            .NotEmpty()
            .WithMessage("A valid 'UserName' is required.");
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("'Email' address is required")
            .EmailAddress().WithMessage("A valid 'Email' is required");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("The 'Password' field is required.")
            .MaximumLength(15)
            .MinimumLength(3)
            .WithMessage("'Password' must be between 3 and 10 characters.");
        RuleFor(u => u.ConfirmPassword)
            .NotEmpty()
            .WithMessage("The 'ConfirmPassword' field is required.");    
        RuleFor(u => u.Password)
            .Equal(u => u.ConfirmPassword)
            .WithMessage("The password does not match.");
    }
}