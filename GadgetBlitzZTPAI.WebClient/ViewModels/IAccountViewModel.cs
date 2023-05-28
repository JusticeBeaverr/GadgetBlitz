using GadgetBlitzZTPAI.WebClient.Models;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public interface IAccountViewModel
    {
        UserModel User { get; set; }

        Task Login(UserModel user);
        bool IsUserAuthenticated { get; set; }
    }
}
