using System.Text;
using System.Text.Json;
using GuitarShopApp.Application.DTO;

namespace GuitarShopApp.WebUI.ApiService;

public class ProductApiService
{
    private readonly HttpClient _httpClient;

    // PropertyName compatibility is set when deserializing from JSON object to C# object.
    // You don't need to use JsonPropertyName attribute on every field.
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
    public async Task<ProductDTO> GetById(int? id)
    {
        var response = await _httpClient.GetAsync($"{id}");
        string apiResponse = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<ProductDTO>(apiResponse, options);

        return product ?? new ProductDTO();
    }
    public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(string category)
    {
        var response = await _httpClient.GetAsync($"get-by-category/{category}");

        string apiResponse = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(apiResponse, options);

        return products ?? new List<ProductDTO>();
    }
    public async Task CreateAsync(ProductDTO model)
    {
        await _httpClient.AddTokenToHeader();
        var createContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("", createContent);
    }
    public async Task UpdateAsync(ProductDTO model)
    {
        await _httpClient.AddTokenToHeader();
        var updateContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        await _httpClient.PutAsync("", updateContent);
    }
    public async Task DeleteAsync(ProductDTO model)
    {
        await _httpClient.AddTokenToHeader();
        await _httpClient.DeleteAsync($"{model.Id}");
    }
}