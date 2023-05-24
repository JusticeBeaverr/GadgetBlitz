using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Queries
{
    public record GetAllSmartphonesQuery() : IRequest<SmartphonesListResponseDTO>
    {

    }

    public class GetAllSmartphonesQueryHandler : IRequestHandler<GetAllSmartphonesQuery, SmartphonesListResponseDTO>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        private readonly IMapper _mapper;

        public GetAllSmartphonesQueryHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }

        public async Task<SmartphonesListResponseDTO> Handle(GetAllSmartphonesQuery request, CancellationToken cancellationToken)
        {
            var smartphones = await _smartphoneRepository.GetAllAsync();
            var result = new SmartphonesListResponseDTO(_mapper.Map<List<SmartphoneResponseDTO>>(smartphones));
            return result;
        }
    }
}
