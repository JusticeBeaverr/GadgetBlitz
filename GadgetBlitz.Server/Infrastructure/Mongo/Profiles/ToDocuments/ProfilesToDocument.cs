using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Infrastructure.Mongo.Document;

namespace GadgetBlitz.Server.Infrastructure.Mongo.Profiles.ToDocuments
{
    public class SmartphoneToSmartphoneDocument: Profile
    {
        public SmartphoneToSmartphoneDocument()
        {
            CreateMap<Smartphone, SmartphoneDocument>();
        }
    }

    public class ColorToColorDocument : Profile
    {
        public ColorToColorDocument()
        {
            CreateMap<Color, ColorDocument>();
        }
    }

    public class CameraToCameraDocument : Profile
    {
        public CameraToCameraDocument()
        {
            CreateMap<Camera, CameraDocument>();
        }
    }
}
