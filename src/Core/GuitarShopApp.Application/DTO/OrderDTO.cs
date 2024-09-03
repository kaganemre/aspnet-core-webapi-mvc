using GuitarShopApp.Domain.Common;

namespace GuitarShopApp.Application.DTO;

public class OrderDTO : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public List<OrderItemDTO> OrderItems { get; set; } = null!;
}

public class OrderItemDTO : BaseEntity
{
    public int ProductId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}