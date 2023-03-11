using GadgetBlitz.Server.Core.Entities;

namespace GadgetBlitz.Server.Application.DTOs
{
    public class SmartphoneDTO
    {
        public Guid SmartphoneID { get; set; }
        public DateTime CreationDate { get; set; }
        public string SmartphoneName { get; set; }
        public string RAM { get; set; }
        public string Memory { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public string TypeOfScreen { get; set; }
        public List<CameraDto> Cameras { get; set; }
        public int CamerasCount { get; set; }
        public List<ColorDto> Colors { get; set; }
        public string PremiereDate { get; set; }
        public float AVGPrice { get; set; }

    }
}
