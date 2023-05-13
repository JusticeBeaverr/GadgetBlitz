using GadgetBlitzZTPAI.WebClient.Models;

namespace GadgetBlitzZTPAI.WebClient.Services.Smartphones
{
    public interface ISmartphonesService
    {
        Task<List<SmartphoneModel>> GetSmartphonesAsync();
        Task<SmartphoneModel> GetSmartphoneByIdAsync(string id);
    }
}
