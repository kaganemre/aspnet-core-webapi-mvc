using AutoMapper;
using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Application.Interfaces.UnitOfWork;
using GuitarShopApp.Persistence.Context;
using GuitarShopApp.Persistence.Repositories;
using GuitarShopApp.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GuitarShopApp.Persistence;

public static class ServiceRegistrationExtension
{
    public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration = null!)
    {
        serviceCollection.AddDbContext<ShopAppDbContext>(options =>
        options.UseSqlServer(configuration["ConnectionStrings:ShopDbConnection"]));
        serviceCollection.AddDbContext<IdentityContext>(options =>
        options.UseSqlServer(configuration["ConnectionStrings:ShopDbConnection"]));
        
        serviceCollection.AddTransient<IProductRepository, ProductRepository>();
        serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddTransient<IOrderRepository, OrderRepository>();
        serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
   
    }
}