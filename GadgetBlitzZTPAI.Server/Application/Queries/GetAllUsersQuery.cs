using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Queries
{
    
        public record GetAllUsersQuery() : IRequest<UsersListResponseDTO>
        {

        }

        public class GetAllSUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UsersListResponseDTO>
        {
            private readonly IUserRepository _userRepository;

            private readonly IMapper _mapper;

            public GetAllSUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UsersListResponseDTO> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var smartphones = await _userRepository.GetAllAsync();
                var result = new UsersListResponseDTO(_mapper.Map<List<UserResponseDTO>>(smartphones));
                return result;
            }
        }
    
}
