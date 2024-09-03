using System.Text;
using System.Text.Json;
using AutoMapper;
using GuitarShopApp.Application.DTO;
using GuitarShopApp.Application.Models;
using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.WebUI.ApiService;

public class UserApiService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
    public UserApiService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<User> GetById(int? id)
    {
        await _httpClient.AddTokenToHeader();
        var response = await _httpClient.GetAsync($"get-by-userid/{id}");
        string apiResponse = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(apiResponse, options);

        return user ?? new User();
    }

    public async Task<User> GetByEmail(string email)
    {
        await _httpClient.AddTokenToHeader();
        var response = await _httpClient.GetAsync($"get-by-email/{Encoding.UTF8.GetString(Encoding.Default.GetBytes(email))}");
        string apiResponse = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(apiResponse, options);

        return user ?? new User();
    }

    public async Task<User> Login(LoginDTO model)
    {
        await _httpClient.AddTokenToHeader();
        var loginContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"check-passwordtouser", loginContent);
        string apiResponse = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(apiResponse, options);

        return user ?? new User();
    }
    public async Task CreateUser(RegisterViewModel model)
    {
        await _httpClient.AddTokenToHeader();
        var content = new StringContent(JsonSerializer.Serialize(_mapper.Map<UserDTO>(model)), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync($"create-user", content);
    }

    public async Task UpdateUser(User model)
    {
        await _httpClient.AddTokenToHeader();
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        await _httpClient.PutAsync($"update-user", content);
    }
}