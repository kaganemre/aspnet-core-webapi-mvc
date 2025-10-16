# 🎸 GuitarShopApp – ASP.NET Core 8 Onion Architecture E-Commerce

🧱 ASP.NET Core Web API + MVC  

💼 E-ticaret ürün yönetimi, kullanıcı rolleri ve ödeme akışı içeren çok katmanlı .NET 8 uygulaması.  

Bu proje, **Onion Architecture** prensipleriyle yapılandırılmış bir **ASP.NET Core 8** uygulamasıdır.  
Onion mimarisi, uygulama katmanları arasındaki bağımlılığı en aza indirerek değişikliklerin en düşük maliyetle yapılabilmesini sağlar.  

Web API ve MVC (UI) katmanları ayrı tutulmuş olup, API üzerinden CRUD işlemlerini destekleyen çok katmanlı bir yapı sunar. MVC projesi API üzerinden verileri alıp dinamik view'lar oluşturur.  

Kullanıcı rollerine göre ürün listeleme, ekleme, güncelleme ve silme işlemleri yapılabilir.  
Ürünler sayfasından ürünler sepete eklenip ödeme yapılabilir.  
Ödeme süreci için **iyzico** entegrasyonu sağlanmıştır.  
Proje, **Docker Compose** ile kolayca ayağa kaldırılabilir.


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

* 🧩 **Domain** → Entity.
* 🧠 **Application** → Use case’ler, DTO’lar, servis interface'leri, validation, AutoMapper profilleri.
* 💾 **Infrastructure** → EF Core implementasyonları, Repository, UnitOfWork, Identity, Hangfire.
* 🌍 **WebAPI / WebUI** → Controller, Middleware, Swagger, Program.cs konfigürasyonu.

> 💡 Katmanlar arası bağımlılıklar ters çevrilerek (Dependency Inversion) **loose coupling** sağlanır.

---

## 🌐 Web API

### GetProducts() metodunda belirlediğimiz alanları veritabanından çekiyoruz.

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

### HttpClient ile API'den tüm ürünleri alıyoruz.

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
        var response = await _httpClient.GetAsync(""); 
        string apiResponse = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(apiResponse, options);

        return products ?? [];
    }
   // Diğer servis metodları burada yer alır
}
```
### API'den alıdığımız ürün listesini view olarak döndürüyoruz.

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
    public async Task<IActionResult> List(string category)
    {
        ViewBag.Categories = await _categoryApiService.GetAll();
        ViewBag.SelectedCategory = RouteData?.Values["category"];

        return View(await _productApiService.GetProductsByCategory(category));
    }
   // Diğer action metodları burada yer alır
}
```
### 🖼️ Ürünler Sayfası

#### Kategoriye bağlı olarak ürünler listelenebilir.
<img width="1637" height="982" alt="image" src="https://github.com/user-attachments/assets/7dab3046-10ae-42d2-b6bb-426e5a8eb8b0" />

---

## 📦 Başlarken

### Clone repo

```bash
$ git clone https://github.com/kaganemre/guitarshop-mvc-api.git
```
### Web API Projesi
Çalıştırmadan önce PostgreSQL de HangfireDB isminde boş bir veritabanı oluşturulması gerekir. dotnet run ile projeyi çalıştırdıktan sonra migration'lar otomatik olarak veritabanına aktarılacaktır.

### MVC Projesi
```bash
$ dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```

Libman paket yöneticisini kurduktan sonra libman restore komutu ile kütüphaneleri yükleyiniz.

---

## 📄 Lisans
Bu proje MIT Lisansı altında lisanslanmıştır.

---
