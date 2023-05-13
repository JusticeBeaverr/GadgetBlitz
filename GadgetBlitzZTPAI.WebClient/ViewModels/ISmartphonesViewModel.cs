using GadgetBlitzZTPAI.WebClient.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public interface ISmartphonesViewModel
    {
        [Parameter]
        public string Id { get; set; }
        int selectedIndex { get; set; }
        Task NavigateToDetails(string id);
        ObservableCollection<SmartphoneModel> Smartphones { get; set; }
        Task GetSmartphones();
    }
}
