using System.Text;
using System.Text.Json;
using GuitarShopApp.Application.DTO;

namespace GuitarShopApp.WebUI.ApiService;

public class OrderApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
    public OrderApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<OrderDTO> CreateAsync(OrderDTO model)
    {
        // Token information is added to the header with this extension method.
        await _httpClient.AddTokenToHeader();
        var createContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("", createContent);
        string apiResponse = await response.Content.ReadAsStringAsync();
        var order = JsonSerializer.Deserialize<OrderDTO>(apiResponse, options);

        return order ?? new();
    }
}