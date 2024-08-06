using FluentValidation;
using GuitarShopApp.Application.Models;

namespace GuitarShopApp.Application.Features.Commands;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordValidator()
    {
        RuleFor(u => u.Token)
            .NotNull()
            .WithMessage("The 'Token' field is required.");
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("'Email' address is required")
            .EmailAddress().WithMessage("A valid email is required");
        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("The 'Password' field is required.")
            .MaximumLength(10)
            .MinimumLength(3)
            .WithMessage("'Password' must be between 3 and 10 characters.");
        RuleFor(u => u.ConfirmPassword)
            .NotNull()
            .WithMessage("The 'ConfirmPassword' field is required.");    
        RuleFor(u => u.Password)
            .Equal(u => u.ConfirmPassword)
            .WithMessage("The password does not match.");
    }
}