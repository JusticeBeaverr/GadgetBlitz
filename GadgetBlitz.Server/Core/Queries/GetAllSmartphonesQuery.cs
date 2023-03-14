using Amazon.Runtime.Internal;
using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitz.Server.Core.Queries
{
    public record GetAllSmartphonesQuery() : IRequest<List<Smartphone>>
    {

    }

    public class GetAllSmartphonesQueryHandler : IRequestHandler<GetAllSmartphonesQuery, List<Smartphone>>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        private readonly IMapper _mapper;

        public GetAllSmartphonesQueryHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }

        public async Task<List<Smartphone>> Handle(GetAllSmartphonesQuery request, CancellationToken cancellationToken)
        {
            var smartphones = await _smartphoneRepository.GetAllAsync();
            List<Smartphone> result = new List<Smartphone>(_mapper.Map<List<Smartphone>>(smartphones));
            return result;
        }
    }
    
}
