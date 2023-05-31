namespace GadgetBlitzZTPAI.WebClient.Services.Auth
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userId = await _localStorageService.GetItemAsync<string>("userId");
            var username = await _localStorageService.GetItemAsync<string>("username");
            var email = await _localStorageService.GetItemAsync<string>("email");
            var token = await _localStorageService.GetItemAsync<string>("token");
            var role = await _localStorageService.GetItemAsync<string>("role"); // Odczytaj rolę z lokalnego magazynu

            // Sprawdź, czy użytkownik jest zalogowany
            if (!string.IsNullOrEmpty(token))
            {
                // Utwórz obiekt ClaimsIdentity dla zalogowanego użytkownika
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email),
             new Claim("Token", $"bearer {token}"),
            new Claim(ClaimTypes.Role, role) // Dodaj claim z rolą
        };

                var identity = new ClaimsIdentity(claims, "Bearer");
                var user = new ClaimsPrincipal(identity);

                // Zwróć zalogowanego użytkownika jako AuthenticationState
                return new AuthenticationState(user);
            }
            else
            {
                // Jeśli użytkownik nie jest zalogowany, zwróć pustą AuthenticationState
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
    }
}
