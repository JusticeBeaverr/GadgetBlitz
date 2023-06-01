using GadgetBlitzZTPAI.WebClient.Models;
using System.Collections.ObjectModel;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public interface IAccountViewModel
    {
        ObservableCollection<UserModel> Users { get; set; }
        UserModel User { get; set; }
        UserModel LogUser { get; set; }

        Task<bool> Login(UserModel user);
        Task<bool> Signup(UserModel user);
        bool IsUserAuthenticated { get; set; }

        Task GetUsers();
        Task GetUser();
        Task DeleteUser(string id);
        Task DeleteMyAccount(string id);
        Task<bool> ChangePassword(ChangePasswordCommand changePasswordCommand);
    }
}
