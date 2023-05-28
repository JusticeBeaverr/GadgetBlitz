using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GadgetBlitzZTPAI.Server.Application.Commands
{
    public record AuthenticateUserCommand(LoginDTO Login) : IRequest<LoginResponseDTO>
    {
        
    }
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, LoginResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthenticateUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            // Pobierz użytkownika o podanej nazwie użytkownika
            var user = await _userRepository.GetByUsernameOrEmail(request.Login.Username, null);
            if (user == null)
            {
                return null;
            }

            // Sprawdź poprawność hasła
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, request.Login.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            // Uwierzytelnianie powiodło się, generuj token JWT
            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDTO(user.UserId, user.Username, user.Email, token);
        }
    }
}
