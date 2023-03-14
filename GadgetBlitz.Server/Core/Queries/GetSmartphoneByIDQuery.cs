using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitz.Server.Core.Queries
{
    public record GetSmartphoneByIDQuery(Guid smartphoneID) : IRequest<Smartphone>
    {
    }

    public class GetSmartphoneByIDQueryHandler : IRequestHandler<GetSmartphoneByIDQuery, Smartphone>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        private readonly IMapper _mapper;

        public GetSmartphoneByIDQueryHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }

        public async Task<Smartphone> Handle(GetSmartphoneByIDQuery request, CancellationToken cancellationToken)
        {
            var smartphone = await _smartphoneRepository.GetByIDAsync(request.smartphoneID);
            return _mapper.Map<Smartphone>(smartphone);
        }
    }
}
