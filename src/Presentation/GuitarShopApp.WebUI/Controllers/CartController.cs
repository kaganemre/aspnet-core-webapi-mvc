using AutoMapper;
using GuitarShopApp.Application.Models;
using GuitarShopApp.Domain.Entities;
using GuitarShopApp.WebUI.ApiService;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShopApp.WebUI.Controllers;

public class CartController : Controller
{
    private readonly ProductApiService _productApiService;
    private readonly IMapper _mapper;
    public CartController(ProductApiService productApiService, CartViewModel cartService, IMapper mapper)
    {
        _productApiService = productApiService;
        Cart = cartService;
        _mapper = mapper;
    }

    public CartViewModel? Cart { get; set; }
    public IActionResult Basket()
    {
        return View(Cart);
    }

    [HttpPost]
    public async Task<IActionResult> Basket(int Id)
    {
        var product = await _productApiService.GetById(Id);
        
        if (product != null)
        {
            var p = _mapper.Map<Product>(product);
            Cart?.AddItem(p, 1);
        }

        return View(Cart);
    }
    public IActionResult Remove(int Id)
    {
        Cart?.RemoveItem(Cart.Items.First(p => p.Product.Id == Id).Product);
        return RedirectToAction("Basket");
    }
}