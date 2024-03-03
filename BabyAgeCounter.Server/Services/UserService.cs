using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;

namespace BabyAgeCounter.Server.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("user");
    }

    public async Task<bool> Authenticate(String token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        try
        {
            await _httpClient.GetStringAsync("auth");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }
}