using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitz.Server.Application.Commands
{
    public record DeleteSmartphoneCommand(Guid smartphoneId) : IRequest
    {
    }

    public class DeleteSmartphoneCommandHandler : IRequestHandler<DeleteSmartphoneCommand>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        private readonly IMapper _mapper;
        public DeleteSmartphoneCommandHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }
        public async Task Handle(DeleteSmartphoneCommand request, CancellationToken cancellationToken)
        {
          

            await _smartphoneRepository.DeleteAsync(request.smartphoneId);
            
        }

        
    }
}
