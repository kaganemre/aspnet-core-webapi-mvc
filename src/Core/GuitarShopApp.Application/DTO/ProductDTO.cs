namespace GuitarShopApp.Application.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}