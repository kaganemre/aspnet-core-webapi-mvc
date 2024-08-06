using GuitarShopApp.Application.Interfaces.Services;

namespace GuitarShopApp.Persistence.Background;

public class Job
{
    private readonly IEmailService _emailService;
    private readonly IProductService _productService;
    public Job(IEmailService emailService, IProductService productService)
    {
        _emailService = emailService;
        _productService = productService;
    }
    public async Task SendNewProductsMailForUsers()
    {
        string message="";
        foreach (var p in await _productService.GetLastFiveProducts())
        {
            message += $"<a href='http://localhost:5191/api/products/{p.Id}'>You can click on the link to view the product<br></a>";
        }
        await _emailService.SendEmailAsync("youremail@outlook.com","New Produtcs", message);
    }

}