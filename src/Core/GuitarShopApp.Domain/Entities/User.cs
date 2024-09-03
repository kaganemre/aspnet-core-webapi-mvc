using GuitarShopApp.Domain.Common;

namespace GuitarShopApp.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RoleName { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
}