using GuitarShopApp.Domain.Common;

namespace GuitarShopApp.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = null!;
}

public class OrderItem : BaseEntity
{
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public double Price { get; set; }
    public int Quantity { get; set; }
}