using GadgetBlitz.Server.Core.Entities;

namespace GadgetBlitz.Server.Core.Repositories
{
    public interface ISmartphoneRepository
    {
        Task<IEnumerable<Smartphone>> GetAllAsync();
        Task<Smartphone>GetByIDAsync(Guid smartphoneID);
        Task AddAsync(Smartphone smartphone);
        Task DeleteAsync(Guid smartphoneID);
        
    }
}
