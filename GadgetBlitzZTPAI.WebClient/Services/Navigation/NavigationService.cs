using Microsoft.AspNetCore.Components;

namespace GadgetBlitzZTPAI.WebClient.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private NavigationManager NavigationManager;

        public NavigationService(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        private class NavigationResult : INavigationResult
        {
            public bool Succes { get; set; }
            public Exception Exception { get; set; } = null;

        }

        public async Task<INavigationResult> NavigateAsync(Uri uri)
        {
            return await NavigateAsync(uri.ToString());
        }

        public async Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters)
        {
            return await NavigateAsync(uri.ToString(), parameters);
        }

        public async Task<INavigationResult> NavigateAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new NavigationResult { Succes = false };
            NavigationManager.NavigateTo(name);
            return new NavigationResult { Succes = true };

        }

        public async Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return await NavigateAsync(name);

            var url = "";
            var firsttime = true;

            foreach (var param in parameters)
            {
                if (firsttime)
                {
                    url += "?" + param.Key + param.Value.ToString();
                    firsttime = false;
                }
                else
                    url += "&" + param.Key + param.Value.ToString();
            }
            return await NavigateAsync(url);

        }
    }
}
