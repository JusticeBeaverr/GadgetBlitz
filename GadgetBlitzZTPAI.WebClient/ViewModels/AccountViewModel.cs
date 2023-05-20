using GadgetBlitzZTPAI.WebClient.Models;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.ViewModels.Base;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public class AccountViewModel : BaseViewModel, IAccountViewModel
    {
        public AccountViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public UserModel User { get; set; } = new UserModel();

        public async Task Login(UserModel user)
        {
            var command = new LoginCommand();
            command.Username = user.UserName;
            command.Password = user.Password;
        }
    }
}
