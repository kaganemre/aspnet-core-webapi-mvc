using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GuitarShopApp.Persistence.Context;

public class IdentitySeedData
{
    private const string adminUser = "admin";
    private const string adminPassword = "Password_536";
    private const string userRole = "admin";

    public static async void IdentityTestUser(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();
        var userManager = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if(context.Database.GetAppliedMigrations().Any())
        {
            await context.Database.MigrateAsync();
        }

        
        var user = await userManager.FindByNameAsync(adminUser);
        
        var role = await roleManager.FindByNameAsync(userRole);
        

        if(user == null)
        {
            user = new IdentityUser 
            {
                UserName = adminUser,
                Email = "info@adminuser.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, adminPassword);
        }

        if(role == null)
        {
            role = new IdentityRole
            {
                Name = userRole
            };

            await roleManager.CreateAsync(role);

            if(role.Name != null)
            await userManager.AddToRoleAsync(user, role.Name);
        }

        
    }
}