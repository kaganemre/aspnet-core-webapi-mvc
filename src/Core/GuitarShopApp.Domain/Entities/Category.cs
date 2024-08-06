using GuitarShopApp.Domain.Common;

namespace GuitarShopApp.Domain.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = null!;
}