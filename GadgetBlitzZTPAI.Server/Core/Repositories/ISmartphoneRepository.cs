using GadgetBlitzZTPAI.Server.Core.Entities;

namespace GadgetBlitzZTPAI.Server.Core.Repositories
{
    public interface ISmartphoneRepository
    {
        Task<List<Smartphone>> GetAllAsync();
        Task<Smartphone> GetByIDAsync(Guid smartphoneID);
        Task AddOrReplaceSmartphoneAsync(Smartphone smartphone);
        Task DeleteAsync(Guid smartphoneID);
        Task<Smartphone> GetByName(string name);

    }
}
