using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShopApp.WebUI.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    public CartController(IProductService productService, CartViewModel cartService)
    {
        _productService = productService;
        Cart = cartService;
    }

    public CartViewModel? Cart { get; set; }
    public IActionResult Basket()
    {
        return View(Cart);
    }

    [HttpPost]
    public async Task<IActionResult> Basket(int Id)
    {
        var product = await _productService.GetById(Id);

        if (product != null)
        {
            Cart?.AddItem(product, 1);
        }

        return View(Cart);
    }
    public IActionResult Remove(int Id)
    {
        Cart?.RemoveItem(Cart.Items.First(p => p.Product.Id == Id).Product);
        return RedirectToAction("Basket");
    }
}