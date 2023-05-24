using System.Text.Json.Serialization;

namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class SmartphoneResponseDTO
    {
        public Guid SmartphoneId { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public DateTime ModificationDate { get; set; }
        [JsonIgnore]
        public int Version { get; set; }
        public string Name { get; set; }
        public string RAM { get; set; }
        public string Memory { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public List<CameraDTO> Cameras { get; set; }
        public int CamerasCount { get; set; }
        public List<ColorDTO> Colors { get; set; }
        public float AVGPrice { get; set; }
        public string PhotoUrl { get; set; }
    }
}
