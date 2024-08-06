using GuitarShopApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarShopApp.Application.Interfaces.Context;

public interface IShopAppContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Order> Orders { get; set; }
}