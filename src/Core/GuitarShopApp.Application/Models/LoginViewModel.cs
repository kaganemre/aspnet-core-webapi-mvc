using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GuitarShopApp.Application.Models;

[ValidateNever]
public class LoginViewModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; } = true;
}