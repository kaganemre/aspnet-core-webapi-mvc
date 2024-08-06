using GuitarShopApp.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuitarShopApp.Infrastructure
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().
            AddJsonFile(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), 
            @"../../Presentation/GuitarShopApp.WebUI/appsettings.Development.json")), optional: true)
            .Build();
            
            services.AddTransient<IEmailService, SmtpEmailService>(i =>
                new SmtpEmailService(
                    configuration["EmailService:Host"],
                    configuration.GetValue<int>("EmailService:Port"),
                    configuration.GetValue<bool>("EmailService:EnableSSL"),
                    configuration["EmailService:Username"],
                    configuration["EmailService:Password"]
                ));

            return services;
        }

    }
}