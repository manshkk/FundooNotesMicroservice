using System.Net.Http.Json;
using CollaborationService.Application.DTOs;
using CollaborationService.Application.Interfaces;

namespace CollaborationService.Infrastructure.Services;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var response = await _httpClient.GetAsync(
            $"api/User/email/{email}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content
            .ReadFromJsonAsync<UserDto>();
    }
}