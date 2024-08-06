using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Models;

public class CartViewModel
{
    public List<CartItemModel> Items { get; set; } = new();
    public virtual void AddItem(Product product, int quantity)
    {
        var item = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();

        if (item == null)
        {
            Items.Add(new CartItemModel { Product = product, Quantity = quantity });
        }
        else
        {
            item.Quantity += quantity;
        }

    }
    public virtual void RemoveItem(Product product)
    {
        Items.RemoveAll(i => i.Product.Id == product.Id);
    }
    public double CalculateTotal()
    {
        return Items.Sum(i => i.Product.Price * i.Quantity);
    }
    public virtual void Clear()
    {
        Items.Clear();
    }
}

public class CartItemModel
{
    public int CartItemId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}