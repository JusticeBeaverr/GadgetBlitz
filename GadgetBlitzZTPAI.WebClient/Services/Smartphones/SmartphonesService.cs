using GadgetBlitzZTPAI.WebClient.Models;

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
            var models = new List<SmartphoneModel>
{
    new SmartphoneModel
    {
        Id = Guid.NewGuid(),
        CreationDate = DateTime.UtcNow,
        SmartphoneName = "Samsung Galaxy S21",
        ImageURL = "https://www.example.com/samsung-galaxy-s21.png",
        RAM = "8 GB",
        Memory = "128 GB",
        ScreenDiagonal = "6.2 inches",
        Resolution = "1080 x 2400 pixels",
        TypeOfScreen = "Dynamic AMOLED 2X",
        Cameras = new List<CameraModel>
        {
            new CameraModel { Mpix = "12 MP", TypeOfCamera = "Wide", WideoResolution = "8K@24fps" },
            new CameraModel { Mpix = "12 MP", TypeOfCamera = "Ultra-Wide", WideoResolution = "4K@60fps" },
            new CameraModel { Mpix = "64 MP", TypeOfCamera = "Telephoto", WideoResolution = "4K@30fps" }
        },
        CamerasCount = 3,
        Colors = new List<ColorModel>
        {
            new ColorModel { Name = "Phantom Gray" },
            new ColorModel { Name = "Phantom White" },
            new ColorModel { Name = "Phantom Pink" },
            new ColorModel { Name = "Phantom Violet" }
        },
        PremiereDate = "January 14, 2021",
        AVGPrice = 799.99f
    },
    new SmartphoneModel
    {
        Id = Guid.NewGuid(),
        CreationDate = DateTime.UtcNow,
        SmartphoneName = "iPhone 13 Pro",
        ImageURL = "https://www.example.com/iphone-13-pro.png",
        RAM = "6 GB",
        Memory = "256 GB",
        ScreenDiagonal = "6.1 inches",
        Resolution = "1170 x 2532 pixels",
        TypeOfScreen = "Super Retina XDR OLED",
        Cameras = new List<CameraModel>
        {
            new CameraModel { Mpix = "12 MP", TypeOfCamera = "Wide", WideoResolution = "4K@60fps" },
            new CameraModel { Mpix = "12 MP", TypeOfCamera = "Ultra-Wide", WideoResolution = "4K@30fps" },
            new CameraModel { Mpix = "12 MP", TypeOfCamera = "Telephoto", WideoResolution = "4K@60fps" }
        },
        CamerasCount = 3,
        Colors = new List<ColorModel>
        {
            new ColorModel { Name = "Graphite" },
            new ColorModel { Name = "Gold" },
            new ColorModel { Name = "Silver" },
            new ColorModel { Name = "Sierra Blue" }
        },
        PremiereDate = "September 14, 2021",
        AVGPrice = 999.99f
    },
    new SmartphoneModel
{
    Id = Guid.NewGuid(),
    CreationDate = DateTime.UtcNow,
    SmartphoneName = "iPhone 13 Pro",
    ImageURL = "https://www.example.com/iphone-13-pro.png",
    RAM = "6 GB",
    Memory = "128 GB",
    ScreenDiagonal = "6.1 inches",
    Resolution = "1170 x 2532 pixels",
    TypeOfScreen = "Super Retina XDR",
    Cameras = new List<CameraModel>
    {
        new CameraModel { Mpix = "12 MP", TypeOfCamera = "Wide", WideoResolution = "4K@60fps" },
        new CameraModel { Mpix = "12 MP", TypeOfCamera = "Ultra-Wide", WideoResolution = "4K@60fps" },
        new CameraModel { Mpix = "12 MP", TypeOfCamera = "Telephoto", WideoResolution = "4K@60fps" }
    },
    CamerasCount = 3,
    Colors = new List<ColorModel>
    {
        new ColorModel { Name = "Graphite" },
        new ColorModel { Name = "Gold" },
        new ColorModel { Name = "Silver" },
        new ColorModel { Name = "Sierra Blue" }
    },
    PremiereDate = "September 14, 2021",
    AVGPrice = 999.00f
},
    new SmartphoneModel
{
    Id = Guid.NewGuid(),
    CreationDate = DateTime.UtcNow,
    SmartphoneName = "Samsung Galaxy S21 Ultra 5G",
    ImageURL = "https://www.example.com/samsung-galaxy-s21-ultra-5g.png",
    RAM = "12 GB",
    Memory = "128 GB",
    ScreenDiagonal = "6.8 inches",
    Resolution = "1440 x 3200 pixels",
    TypeOfScreen = "Dynamic AMOLED 2X",
    Cameras = new List<CameraModel>
    {
        new CameraModel { Mpix = "108 MP", TypeOfCamera = "Wide", WideoResolution = "8K@24fps" },
        new CameraModel { Mpix = "10 MP", TypeOfCamera = "Periscope Telephoto", WideoResolution = "4K@60fps" },
        new CameraModel { Mpix = "12 MP", TypeOfCamera = "Ultra-Wide", WideoResolution = "4K@60fps" }
    },
    CamerasCount = 4,
    Colors = new List<ColorModel>
    {
        new ColorModel { Name = "Phantom Black" },
        new ColorModel { Name = "Phantom Silver" },
        new ColorModel { Name = "Phantom Titanium" },
        new ColorModel { Name = "Phantom Navy" }
    },
    PremiereDate = "January 29, 2021",
    AVGPrice = 1199.00f
}
            };

            return models;








            /*try
            {
                var response = await _httpClient.GetStringAsync("");
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                    PropertyNameCaseInsensitive = true,
                };
                var smartphones = JsonSerializer.Deserialize<List<SmartphoneModel>>(response, options);
                return smartphones;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }*/
        }

        public Task<SmartphoneModel> GetSmartphoneByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
