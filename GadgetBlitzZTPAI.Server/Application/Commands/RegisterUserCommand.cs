using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GadgetBlitzZTPAI.Server.Application.Commands
{
    public record RegisterUserCommand(RegisterDTO registerDto) : IRequest<User>
    {
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Sprawdź, czy użytkownik o podanej nazwie użytkownika już istnieje
            var existingUser = await _userRepository.GetByUsernameOrEmail(request.registerDto.Username, request.registerDto.Email);
            if (existingUser != null)
            {
                throw new ApplicationException("Użytkownik o podanej nazwie użytkownika już istnieje.");
            }
           
            var passwordHash = _passwordHasher.HashPassword(null, request.registerDto.Password);            
            var newUser = new User(request.registerDto.Username,request.registerDto.Email,passwordHash);
            await _userRepository.AddOrReplaceUserAsync(newUser);

            return newUser;
        }

        
    }
}
