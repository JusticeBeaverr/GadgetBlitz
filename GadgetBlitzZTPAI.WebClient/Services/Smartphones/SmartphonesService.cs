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

        public SmartphonesService(HttpClient httpClient)
        {
            _httpClient = httpClient;

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
    }
}
