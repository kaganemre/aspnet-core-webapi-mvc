using System.Text.Json.Serialization;
using GuitarShopApp.Application.Models;
using GuitarShopApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GuitarShopApp.Persistence.Services;
public class SessionCartService : CartViewModel
{
    public static CartViewModel GetCart(IServiceProvider services)
    {
        ISession? session = services.CreateScope().ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        SessionCartService cart = session?.GetJson<SessionCartService>("Cart") ?? new SessionCartService();
        cart.Session = session;
        return cart;
    }

    [JsonIgnore]
    public ISession? Session { get; set; }
    public override void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.SetJson("Cart", this);
    }
    public override void RemoveItem(Product product)
    {
        base.RemoveItem(product);
        Session?.SetJson("Cart", this);
    }
    public override void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
}