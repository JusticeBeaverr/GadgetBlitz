using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;

namespace GadgetBlitzZTPAI.Server.Infrastructure.Profiles
{
    public class SmartphoneDTOToSmartphone : Profile
    {
        public SmartphoneDTOToSmartphone()
        {
            CreateMap<SmartphoneDTO, Smartphone>();
        }
    }

    public class ColorDTOToColor : Profile
    {
        public ColorDTOToColor()
        {
            CreateMap<ColorDTO, Color>();
        }
    }

    public class CameraDTOToCamera : Profile
    {
        public CameraDTOToCamera()
        {
            CreateMap<CameraDTO, Camera>();
        }
    }

    public class UserResponseDTOToUser : Profile
    {
        public UserResponseDTOToUser()
        {
            CreateMap<UserResponseDTO, User>();
        }
    }
    
    
}
