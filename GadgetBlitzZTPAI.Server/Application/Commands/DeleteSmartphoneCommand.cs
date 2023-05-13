using AutoMapper;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Commands
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


        public async Task<Unit> Handle(DeleteSmartphoneCommand request, CancellationToken cancellationToken)
        {
            await _smartphoneRepository.DeleteAsync(request.smartphoneId);
            return Unit.Value;
        }

       
    }
}
