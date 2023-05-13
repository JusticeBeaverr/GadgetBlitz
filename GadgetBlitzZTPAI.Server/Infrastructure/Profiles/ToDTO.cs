using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;

namespace GadgetBlitzZTPAI.Server.Infrastructure.Profiles
{
    public class SmartphoneToSmartphoneDTO : Profile
    {
        public SmartphoneToSmartphoneDTO()
        {
            CreateMap<Smartphone, SmartphoneDTO>();
        }
    }

    public class ColorToColorDTO : Profile
    {
        public ColorToColorDTO()
        {
            CreateMap<Color, ColorDTO>();
        }
    }

    public class CameraToCameraDTO : Profile
    {
        public CameraToCameraDTO()
        {
            CreateMap<Camera, CameraDTO>();
        }
    }
}
