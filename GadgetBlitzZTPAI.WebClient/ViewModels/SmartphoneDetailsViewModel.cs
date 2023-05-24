using GadgetBlitzZTPAI.WebClient.Models;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.Services.Smartphones;
using GadgetBlitzZTPAI.WebClient.ViewModels.Base;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public class SmartphoneDetailsViewModel : BaseViewModel, ISmartphoneDetailsViewModel
    {
        private readonly ISmartphonesService _smartphonesService;
        public SmartphoneDetailsViewModel(INavigationService navigationService, ISmartphonesService smartphonesService) : base(navigationService)
        {
            _smartphonesService = smartphonesService;
        }

        public string SmartphoneId { get; set; }
        public SmartphoneModel Model {get; set;} = new SmartphoneModel();
        public List<CameraModel> Cameras { get; set; } = new List<CameraModel>();
        public List<ColorModel> Colors { get; set; } = new List<ColorModel>();


        public async Task GetSmartphoneById(string id)
        {
            Model = await _smartphonesService.GetSmartphoneByIdAsync(id);
        }
    }
}
