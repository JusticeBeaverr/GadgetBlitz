using GadgetBlitzZTPAI.Server.Core.Entities;

namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class SmartphonesListResponseDTO
    {
        public List<SmartphoneDTO> Items { get; set; }

        public SmartphonesListResponseDTO(List<SmartphoneDTO> items)
        {
            Items = items;
        }
    }
}
