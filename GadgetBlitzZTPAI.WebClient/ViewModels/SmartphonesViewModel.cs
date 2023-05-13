using GadgetBlitzZTPAI.WebClient.Models;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.Services.Smartphones;
using GadgetBlitzZTPAI.WebClient.ViewModels.Base;
using System.Collections.ObjectModel;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public class SmartphonesViewModel : BaseViewModel, ISmartphonesViewModel
    {
        private readonly ISmartphonesService smartphonesService;
        public ObservableCollection<SmartphoneModel> Smartphones { get; set; } = new ObservableCollection<SmartphoneModel>();
        public string Id { get; set; }
        public int SelectedIndex { get; set; }
        public int selectedIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SmartphonesViewModel(INavigationService navigationService, ISmartphonesService smartphonesService) : base(navigationService)
        {
            this.smartphonesService = smartphonesService;
            GetSmartphones();
        }

        public Task NavigateToDetails(string id)
        {
            throw new NotImplementedException();
        }

        public async Task GetSmartphones()
        {
            var smartphones = await smartphonesService.GetSmartphonesAsync();
            foreach (var smartphone in smartphones)
            {
                Smartphones.Add(smartphone);
            }
        }
    }
}
