# 🎸 GuitarShopApp

**🧱 ASP.NET Core Web API + MVC**

Bu proje, **Onion Architecture** prensipleriyle yapılandırılmış bir **ASP.NET Core 8** uygulamasıdır. Onion mimarisi, uygulama katmanları arasındaki bağımlılığı en aza indirerek değişikliklerin en düşük maliyetle yapılabilmesini sağlar. Web API ve MVC (UI) katmanları ayrı tutulmuş olup, API üzerinden CRUD işlemlerini destekleyen çok katmanlı bir yapı sunar. MVC projesi API üzerinden verileri alıp dinamik view'lar oluşturur.

Kullanıcı rollerine göre ürün listeleme, ekleme, güncelleme ve silme işlemleri yapılabilir. Ürünler sayfasından ürünler sepete eklenip ödeme yapılabilir. Ödeme süreci için iyzico entegrasyonu sağlanmıştır. Proje, Docker Compose ile kolayca ayağa kaldırılabilir.

---

## 🚀 Teknolojiler ve Özellikler

* 🧩 .NET 8
* 🧅 Onion Architecture (Domain / Application / Infrastructure / Web)
* 🗃️ Entity Framework Core (PostgreSQL)
* 🧰 Repository & Unit of Work Pattern
* 🔐 Identity (Cookie + JWT tabanlı kimlik doğrulama)
* 🧾 Authorization
* 🔁 AutoMapper
* ✅ FluentValidation
* ⚙️ Hangfire
* 📜 Swagger
* 🔄 CORS yapılandırması
* 💻 Bootstrap 5 + AJAX UI entegrasyonu
* 🐳 Dockerfile (WebAPI / WebUI) ve `docker-compose.yml`

📁 **Ana dosyalar:** `GuitarShopApp.sln`, `docker-compose.yml`, `Dockerfile-WebAPI`, `Dockerfile-WebUI`

---

## 🧱 Mimari ve Proje Yapısı

Proje **Onion Architecture**’a göre katmanlara ayrılmıştır:

* 🧩 **Domain** → Entity, Value Object, Domain servisleri, interface’ler.
* 🧠 **Application** → Use case’ler, DTO’lar, servis interface'leri, validation, AutoMapper profilleri.
* 💾 **Infrastructure** → EF Core implementasyonları, Repository, UnitOfWork, Identity, Hangfire.
* 🌍 **WebAPI / WebUI** → Controller, Middleware, Swagger, Program.cs konfigürasyonu.

> 💡 Katmanlar arası bağımlılıklar ters çevrilerek (Dependency Inversion) **loose coupling** sağlanır.

---

## 🌐 Web API

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts() // DTO ile belirlediğimiz alanları veritabanından çekiyoruz.
    {
        return Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productService.GetAll()));
    }
    // Diğer action metodları burada yer alır
}
```
---

## 🖥️ Web UI (MVC)

```csharp
public class ProductApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
        var response = await _httpClient.GetAsync(""); HttpClient ile API'den tüm ürünleri alıyoruz.
        string apiResponse = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(apiResponse, options);

        return products ?? [];
    }
   // Diğer servis metodları burada yer alır
}
```
```csharp
[Authorize(Roles = "admin")]
public class HomeController : Controller
{
    private readonly ProductApiService _productApiService;
    private readonly CategoryApiService _categoryApiService;
    private readonly IMapper _mapper;
    public HomeController(ProductApiService productApiService, CategoryApiService categoryApiService, IMapper mapper)
    {
        _productApiService = productApiService;
        _categoryApiService = categoryApiService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _productApiService.GetAll()); 
       // HttpClient ile API'den alıdığımız ürün listesini view olarak döndürüyoruz.
    }
   // Diğer action metodları burada yer alır
}
```
---
