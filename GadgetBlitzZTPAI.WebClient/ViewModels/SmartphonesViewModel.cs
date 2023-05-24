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

        public async Task NavigateToDetails(string id)
        {
            await NavigationService.NavigateAsync($"/details/{id}");
        }

        public async Task GetSmartphones()
        {
            var smartphones = await smartphonesService.GetSmartphonesAsync();
            foreach (var smartphone in smartphones)
            {
                Smartphones.Add(smartphone);
            }
        }


        public string SearchText { get; set; }

        // Implementacja innych metod interfejsu

        public void FilterSmartphones()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                var filteredSmartphones = Smartphones.Where(s => s.Name.Contains(SearchText)).ToList();
                Smartphones = new ObservableCollection<SmartphoneModel>(filteredSmartphones);
            }
            else
            {
                // Jeśli pole wyszukiwania jest puste, przywróć pełną listę smartfonów
                // Możesz na przykład wczytać je ponownie z bazy danych lub innych źródeł
            }
        }
    }
}
