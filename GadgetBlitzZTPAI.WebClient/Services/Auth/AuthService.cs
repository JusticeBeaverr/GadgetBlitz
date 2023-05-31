using GadgetBlitzZTPAI.WebClient.Models;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Linq.Dynamic.Core.Tokenizer;

namespace GadgetBlitzZTPAI.WebClient.Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly NavigationManager navigationManager;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task DeleteUser(string id)
        {
             var token = await _localStorageService.GetItemAsync<string>("token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"userid?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException();
            }
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await _httpClient.GetStringAsync("users");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true,
                };
                var users = JsonSerializer.Deserialize<List<UserModel>>(response, options);
                return users;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
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
                    var role = responseObj.RootElement.GetProperty("role").GetString();

                    // Zapisz token, ID, nazwę użytkownika i adres e-mail w sesji lub innym miejscu, w zależności od implementacji
                    await _localStorageService.SetItemAsync("userId", userId);
                    await _localStorageService.SetItemAsync("username", username);
                    await _localStorageService.SetItemAsync("email", email);
                    await _localStorageService.SetItemAsync("token", token);
                    await _localStorageService.SetItemAsync("role", role);
                    

                    // Utwórz obiekt ClaimsIdentity dla zalogowanego użytkownika
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim("Token", token)
            };

                    var identity = new ClaimsIdentity(claims, "Bearer");
                    var user = new ClaimsPrincipal(identity);

                    // Utwórz obiekt AuthenticationState na podstawie ClaimsPrincipal
                    var authenticationState = new AuthenticationState(user);

                    // Zaktualizuj stan uwierzytelnienia za pomocą AuthenticationStateProvider
                    await _authenticationStateProvider.GetAuthenticationStateAsync();

                    return true;
                    navigationManager.NavigateTo("/", true);
                }
            }

            // Logowanie nie powiodło się
            return false;
        }

        public async void Logout()
        {
            await _localStorageService.RemoveItemAsync("userId");
            await _localStorageService.RemoveItemAsync("username");
            await _localStorageService.RemoveItemAsync("email");
            await _localStorageService.RemoveItemAsync("token");

            // Utwórz pustą AuthenticationState, aby wylogować użytkownika
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var anonymousState = new AuthenticationState(anonymousUser);

            // Zaktualizuj stan uwierzytelnienia za pomocą AuthenticationStateProvider
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<HttpResponseMessage> Register(RegisterUserCommand registerUserCommand)
        {
            var json = JsonSerializer.Serialize(registerUserCommand);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("register", content);
            return response;
        }

        public async Task<bool> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                var json = JsonSerializer.Serialize(changePasswordCommand);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("changepassword", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
