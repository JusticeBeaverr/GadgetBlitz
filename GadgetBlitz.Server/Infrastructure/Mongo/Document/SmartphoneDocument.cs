

namespace GadgetBlitz.Server.Infrastructure.Mongo.Document
{
    public class SmartphoneDocument
    {
        public Guid SmartphoneID { get; set; }
        public DateTime CreationDate { get; set; }
        public string SmartphoneName { get; set; }
        public string RAM { get; set; }
        public string Memory { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public string TypeOfScreen { get; set; }
        public List<CameraDocument> Cameras { get; set; }
        public int CamerasCount { get; set; }
        public List<ColorDocument> Colors { get; set; }
        public string PremiereDate { get; set; }
        public float AVGPrice { get; set; }
    }
}
