using GadgetBlitzZTPAI.WebClient.Models;
using GadgetBlitzZTPAI.WebClient.Services.Auth;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.ViewModels.Base;
using Radzen;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public class AccountViewModel : BaseViewModel, IAccountViewModel
    {
        private readonly IAuthService _authService;
        
        public bool IsUserAuthenticated { get; set; }
        public AccountViewModel(INavigationService navigationService, IAuthService authService) : base(navigationService)
        {
            _authService = authService;
            
        }

        public UserModel User { get; set; } = new UserModel();

        public async Task Login(UserModel user)
        {
            var command = new LoginCommand();
            command.Username = user.UserName;
            command.Password = user.Password;

            IsUserAuthenticated =  await _authService.Login(command);
            
            NavigationService.NavigateAsync("/");
            
        }
    }
}
