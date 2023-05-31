using AutoMapper;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Commands
{
    public record DeleteUserCommand(Guid userId) : IRequest
    {
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.userId);
            return Unit.Value;
        }


    }
}
