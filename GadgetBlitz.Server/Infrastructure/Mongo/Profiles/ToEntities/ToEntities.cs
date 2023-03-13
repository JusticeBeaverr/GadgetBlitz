using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Infrastructure.Mongo.Document;

namespace GadgetBlitz.Server.Infrastructure.Mongo.Profiles.ToEntities
{
    public class SmartphoneDocumentToSmartphone : Profile
    {
        public SmartphoneDocumentToSmartphone()
        {
            CreateMap<SmartphoneDocument, Smartphone>();
        }
    }

    public class ColorDocumentToColor : Profile
    {
        public ColorDocumentToColor()
        {
            CreateMap<ColorDocument, Color>();
        }
    }

    public class CameraDocumentToCamera : Profile
    {
        public CameraDocumentToCamera()
        {
            CreateMap<CameraDocument, Camera>();
        }
    }
}
