using GuitarShopApp.Domain.Common;

namespace GuitarShopApp.Domain.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public bool IsHome { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}