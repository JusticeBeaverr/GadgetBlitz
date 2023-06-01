using Blazored.LocalStorage;
using GadgetBlitzZTPAI.WebClient.Models;
using GadgetBlitzZTPAI.WebClient.Services.Auth;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.Services.Smartphones;
using GadgetBlitzZTPAI.WebClient.ViewModels.Base;
using GadgetBlitzZTPAI.WebClient.Views;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Collections.ObjectModel;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public class AccountViewModel : BaseViewModel, IAccountViewModel
    {
        private readonly IAuthService _authService;
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager navigationManager;
        public ObservableCollection<UserModel> Users { get; set; } = new ObservableCollection<UserModel>();
        public bool IsUserAuthenticated { get; set; }
        public AccountViewModel(INavigationService navigationService, IAuthService authService, ILocalStorageService localStorageService) : base(navigationService)
        {
            _authService = authService;
            _localStorageService = localStorageService;
             GetUsers();

        }

        public UserModel User { get; set; } = new UserModel();
        public UserModel LogUser { get; set; } = new UserModel();

        public async Task<bool> Login(UserModel user)
        {
            var command = new LoginCommand();
            command.Username = user.UserName;
            command.Password = user.Password;

            IsUserAuthenticated =  await _authService.Login(command);

            var userId = await _localStorageService.GetItemAsync<Guid>("userId");
            var username = await _localStorageService.GetItemAsync<string>("username");
            var email = await _localStorageService.GetItemAsync<string>("email");
            var token = await _localStorageService.GetItemAsync<string>("token");

           
            
            return IsUserAuthenticated;

        }

        public async Task<bool> Signup(UserModel user)
        {
            var command = new RegisterUserCommand();
            command.Username = user.UserName;
            command.Email = user.Email;
            command.Password = user.Password;
            var response = await _authService.Register(command);
            return response.IsSuccessStatusCode;
        }

        public async Task GetUsers()
        {
            var users = await _authService.GetUsersAsync();
            Users = new ObservableCollection<UserModel>(users);
        }

        public async Task GetUser()
        {
            var userId = await _localStorageService.GetItemAsync<Guid>("userId");
            var username = await _localStorageService.GetItemAsync<string>("username");
            var email = await _localStorageService.GetItemAsync<string>("email");
            var token = await _localStorageService.GetItemAsync<string>("token");

            // Utwórz obiekt UserModel na podstawie pobranych danych
            LogUser.UserId = userId;
            LogUser.UserName = username;
            LogUser.Email = email;
            LogUser.Token = token;
        }

        public async Task DeleteUser(string id)
        {
            await _authService.DeleteUser(id);
            Users.Clear();
            await GetUsers();
        }
        public async Task DeleteMyAccount(string id)
        {
            await _authService.DeleteUser(id);

        }

        public async Task<bool> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            var response = await _authService.ChangePassword(changePasswordCommand);
            return response;
        }
    }
}
