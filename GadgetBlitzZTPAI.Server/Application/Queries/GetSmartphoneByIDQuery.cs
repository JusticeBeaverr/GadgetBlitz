using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Queries
{
    public record GetSmartphoneByIDQuery(Guid smartphoneID) : IRequest<SmartphoneResponseDTO>
    {
    }

    public class GetSmartphoneByIDQueryHandler : IRequestHandler<GetSmartphoneByIDQuery, SmartphoneResponseDTO>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        private readonly IMapper _mapper;

        public GetSmartphoneByIDQueryHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }

        public async Task<SmartphoneResponseDTO> Handle(GetSmartphoneByIDQuery request, CancellationToken cancellationToken)
        {
            var smartphone = await _smartphoneRepository.GetByIDAsync(request.smartphoneID);
            return _mapper.Map<SmartphoneResponseDTO>(smartphone);
        }
    }
}
