using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GuitarShopApp.Persistence.Context;

public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T>
    where T : DbContext
{
    
    public T CreateDbContext(string[] args)
    {

        IConfigurationRoot configuration = new ConfigurationBuilder().
        AddJsonFile(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), 
        @"../../Presentation/GuitarShopApp.WebAPI/appsettings.Development.json")), optional: true)
        .Build();

        var builder = new DbContextOptionsBuilder<T>();
        builder.UseSqlServer(configuration["ConnectionStrings:ShopDbConnection"]);
        
        var dbContext = (T?)Activator.CreateInstance(typeof(T), builder.Options);
        
        return dbContext ?? dbContext!;
    }
}