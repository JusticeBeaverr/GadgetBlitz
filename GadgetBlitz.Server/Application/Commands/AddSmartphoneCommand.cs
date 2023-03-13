using Amazon.Runtime.Internal;
using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace GadgetBlitz.Server.Application.Commands
{
    public record AddSmartphoneCommand(Smartphone smartphone) : IRequest<Smartphone> {
    }

    public class AddSmartphoneCommandHandler : IRequestHandler<AddSmartphoneCommand, Smartphone>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;
        
        private readonly IMapper _mapper;
        public AddSmartphoneCommandHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }
        public async Task<Smartphone> Handle(AddSmartphoneCommand request, CancellationToken cancellationToken)
        {
            var smartphone = _mapper.Map<Smartphone>(request.smartphone);
            smartphone.SmartphoneID = Guid.NewGuid();
            smartphone.CreationDate = DateTime.UtcNow;
            smartphone.Colors.ToList();
            smartphone.Cameras.ToList();

            await _smartphoneRepository.AddAsync(smartphone);
            return smartphone;
        }
    }
}
