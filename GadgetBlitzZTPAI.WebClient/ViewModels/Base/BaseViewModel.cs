using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GadgetBlitzZTPAI.WebClient.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; }
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

        }

        
        public event PropertyChangedEventHandler PropertyChanged;
      
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(
            ref T backingFiled,
            T value,

            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
            backingFiled = value;
            OnPropertyChanged(propertyName);
        }


    }
}
