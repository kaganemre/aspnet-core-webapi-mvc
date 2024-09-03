using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuitarShopApp.Persistence.Context;

public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T>
    where T : DbContext
{
    
    public T CreateDbContext(string[] args)
    {

        // IConfigurationRoot configuration = new ConfigurationBuilder().
        // AddJsonFile(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), 
        // @"../../Presentation/GuitarShopApp.WebAPI/appsettings.Development.json")), optional: true)
        // .Build();
        
        var _accessor = new HttpContextAccessor();
        var env = _accessor.HttpContext?.RequestServices.GetRequiredService<IWebHostEnvironment>();
        
        string server="", port = "";

        if(env?.EnvironmentName == "Development")
        {
            server="localhost";
            port = "5433";
        }
        else
        {
            server="app_db";
            port = "5432";
        }
            

        var connectionString = $"User ID=postgres;Password=postgres;Server={server};Port={port};Database=GuitarShopApp;Pooling=true";

        var builder = new DbContextOptionsBuilder<T>();
        builder.UseNpgsql(connectionString);
        
        var dbContext = (T?)Activator.CreateInstance(typeof(T), builder.Options);
        
        return dbContext ?? dbContext!;
    }
}