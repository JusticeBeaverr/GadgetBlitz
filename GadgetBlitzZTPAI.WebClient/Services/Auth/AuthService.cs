using GadgetBlitzZTPAI.WebClient.Models;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;

namespace GadgetBlitzZTPAI.WebClient.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<bool> Login(LoginCommand loginCommand)
        {
            var json = JsonSerializer.Serialize(loginCommand);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObj = JsonDocument.Parse(responseJson);

                // Odczytaj token z pola "token" w odpowiedzi
                if (responseObj.RootElement.TryGetProperty("token", out var tokenElement))
                {
                    var token = tokenElement.GetString();

                    // Odczytaj inne informacje użytkownika z odpowiedzi
                    var userId = responseObj.RootElement.GetProperty("userId").GetString();
                    var username = responseObj.RootElement.GetProperty("username").GetString();
                    var email = responseObj.RootElement.GetProperty("email").GetString();

                    // Zapisz token, ID, nazwę użytkownika i adres e-mail w sesji lub innym miejscu, w zależności od implementacji

                    
                    await _localStorageService.SetItemAsync("userId", userId);
                    await _localStorageService.SetItemAsync("username", username);
                    await _localStorageService.SetItemAsync("email", email);
                    await _localStorageService.SetItemAsync("token", token);

                    return true;
                }
            }

            // Logowanie nie powiodło się
            return false;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterUserCommand registerUserCommand)
        {
            throw new NotImplementedException();
        }
    }
}
