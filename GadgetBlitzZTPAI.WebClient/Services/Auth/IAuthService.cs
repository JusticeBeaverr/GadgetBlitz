using GadgetBlitzZTPAI.WebClient.Models;

namespace GadgetBlitzZTPAI.WebClient.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> Login(LoginCommand loginCommand);
        Task Register(RegisterUserCommand registerUserCommand);
        void Logout();
    }
}
