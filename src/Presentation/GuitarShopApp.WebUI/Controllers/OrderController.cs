using AutoMapper;
using GuitarShopApp.Application.DTO;
using GuitarShopApp.Application.Models;
using GuitarShopApp.WebUI.ApiService;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShopApp.WebUI.Controllers;

public class OrderController : Controller
{
    private readonly CartViewModel cart;
    private readonly OrderApiService _orderApiService;
    private readonly UserApiService _userApiService;
    private readonly IMapper _mapper;
    public OrderController(CartViewModel cartService, 
    OrderApiService orderApiService, 
    IMapper mapper,
    UserApiService userApiService)
    {
        cart = cartService;
        _orderApiService = orderApiService;
        _userApiService = userApiService;
        _mapper = mapper;
    }
    public IActionResult Checkout()
    {
        return View(new OrderModel { Cart = cart });
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(OrderModel model, string email)
    {
        if (cart.Items.Count == 0)
        {
            ModelState.AddModelError("", "There are no items in your cart.");
        }

        if (ModelState.IsValid)
        {
            var order = _mapper.Map<OrderDTO>(model);
            order.OrderDate = DateTime.Now;
            var user = await _userApiService.GetByEmail(email);
            order.UserId = user.Id.ToString();
            order.OrderItems = cart.Items.Select(i => new OrderItemDTO
            {
                ProductId = i.Product.Id,
                Price = i.Product.Price,
                Quantity = i.Quantity
            }).ToList();

            model.Cart = cart;
            var payment = ProcessPayment(model);
            if (payment.Status == "success")
            {
                var currentOrder = await _orderApiService.CreateAsync(order);
                cart.Clear();
                return RedirectToAction("Completed", new { OrderId = currentOrder.Id });
            }
            model.Cart = cart;
            return View(model);
        }
        else
        {
            model.Cart = cart;
            return View(model);
        }

    }
    private Payment ProcessPayment(OrderModel model)
    {
        Options options = new Options();
        options.ApiKey = "";
        options.SecretKey = "";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = new Random().Next(111111111, 999999999).ToString();
        request.Price = model?.Cart?.CalculateTotal().ToString();
        request.PaidPrice = model?.Cart?.CalculateTotal().ToString(); ;
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model?.CartName;
        paymentCard.CardNumber = model?.CartNumber;
        paymentCard.ExpireMonth = model?.ExpirationMonth;
        paymentCard.ExpireYear = model?.ExpirationYear;
        paymentCard.Cvc = model?.Cvc;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = model?.Name;
        buyer.Surname = model?.Name;
        buyer.GsmNumber = model?.Phone;
        buyer.Email = model?.Email;
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = model?.AddressLine;
        buyer.Ip = "85.34.78.112";
        buyer.City = model?.City;
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = model?.Name;
        shippingAddress.City = model?.City;
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = model?.AddressLine;
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = model?.Name;
        billingAddress.City = model?.City;
        billingAddress.Country = "Turkey";
        billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in model?.Cart?.Items ?? Enumerable.Empty<CartItemModel>())
        {
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = item.Product.Id.ToString();
            firstBasketItem.Name = item.Product.Name;
            firstBasketItem.Category1 = item.Product.CategoryId.ToString();
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = item.Product.Price.ToString();
            basketItems.Add(firstBasketItem);
        }



        request.BasketItems = basketItems;

        Payment payment = Payment.Create(request, options);
        return payment;
    }
    public IActionResult Completed(int orderId)
    {
        return View(orderId);
    }
}