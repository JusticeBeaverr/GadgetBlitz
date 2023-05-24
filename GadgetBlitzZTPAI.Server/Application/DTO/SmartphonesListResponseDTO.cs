using GadgetBlitzZTPAI.Server.Core.Entities;

namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class SmartphonesListResponseDTO
    {
        public List<SmartphoneResponseDTO> Items { get; set; }

        public SmartphonesListResponseDTO(List<SmartphoneResponseDTO> items)
        {
            Items = items;
        }
    }
}
