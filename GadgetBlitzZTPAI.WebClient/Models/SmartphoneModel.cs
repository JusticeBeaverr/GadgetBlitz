namespace GadgetBlitzZTPAI.WebClient.Models
{
    public class SmartphoneModel
    {
        public Guid SmartphoneId { get; set; }
        public string Name { get; set; } 
        public string RAM { get; set; }
        public string Memory { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public List<CameraModel> Cameras { get; set; }
        public int CamerasCount { get; set; }
        public List<ColorModel> Colors { get; set; }
        public float AVGPrice { get; set; }
        public string PhotoUrl { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}
