using GadgetBlitzZTPAI.WebClient.Models;

namespace GadgetBlitzZTPAI.WebClient.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> Login(LoginCommand loginCommand);
        Task<HttpResponseMessage> Register(RegisterUserCommand registerUserCommand);
        void Logout();
        Task<List<UserModel>> GetUsersAsync();
        Task DeleteUser(string id);
        Task<bool> ChangePassword(ChangePasswordCommand changePasswordCommand);
    }
}
