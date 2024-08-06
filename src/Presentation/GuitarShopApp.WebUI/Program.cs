using GuitarShopApp.Infrastructure;
using GuitarShopApp.Persistence;
using GuitarShopApp.Application;
using GuitarShopApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using GuitarShopApp.Application.Models;
using GuitarShopApp.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
                        .AddSignInManager<SignInManager<IdentityUser>>()
                        .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
                        
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartViewModel>(scs => SessionCartService.GetCart(scs));


builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});
 
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    
    options.User.RequireUniqueEmail = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.SignIn.RequireConfirmedEmail = true;

});

        
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute("products", "products/{category?}", new { controller = "Home", action = "List"});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


IdentitySeedData.IdentityTestUser(app.Services);

app.Run();
