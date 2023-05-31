using Blazored.LocalStorage;
using GadgetBlitzZTPAI.WebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GadgetBlitzZTPAI.WebClient.Services.Smartphones
{
    public class SmartphonesService : ISmartphonesService
    {

        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly ILocalStorageService _localStorageService;

        public SmartphonesService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<List<SmartphoneModel>> GetSmartphonesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("smartphones");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true,
                };
                var smartphones = JsonSerializer.Deserialize<List<SmartphoneModel>>(response, options);
                return smartphones;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<SmartphoneModel> GetSmartphoneByIdAsync(string id)
        {
            var responsestring = await _httpClient.GetStringAsync($"id?id={id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true,
            };
            var smartphone = JsonSerializer.Deserialize<SmartphoneModel>(responsestring, options);

            return smartphone;
        }

        public async Task DeleteSmartphone(string id)
        {
            var token = await _localStorageService.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"smartphoneid?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException();
            }
        }
    }
}
