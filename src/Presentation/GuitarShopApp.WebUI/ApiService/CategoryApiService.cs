using System.Text.Json;
using GuitarShopApp.Application.DTO;

namespace GuitarShopApp.WebUI.ApiService;

public class CategoryApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
    public CategoryApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAll()
    {
        var response = await _httpClient.GetAsync("");
        string apiResponse = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(apiResponse, options);

        return products ?? [];
    }


}