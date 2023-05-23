using GadgetBlitzZTPAI.Server.Core.Entities;

namespace GadgetBlitzZTPAI.Server.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddOrReplaceUserAsync(User user);
        Task DeleteAsync(Guid userId);
        Task<User> GetByUsernameOrEmail(string name, string email);
    }
}
