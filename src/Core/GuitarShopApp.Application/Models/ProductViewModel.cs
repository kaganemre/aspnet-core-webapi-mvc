using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GuitarShopApp.Application.Models;

[ValidateNever]
public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int CategoryId { get; set; }

}