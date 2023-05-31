using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GadgetBlitzZTPAI.Server.Application.Commands
{
    public record ChangePasswordCommand(ChangePasswordDTO ChangePasswordDto) : IRequest<bool>;

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserById(request.ChangePasswordDto.UserId);
                if (user != null)
                {
                    var isOldPasswordCorrect = user.VerifyPassword(request.ChangePasswordDto.OldPassword);
                    if (isOldPasswordCorrect)
                    {
                        var newPasswordHash = _passwordHasher.HashPassword(user, request.ChangePasswordDto.NewPassword);
                        user.PasswordHash = newPasswordHash;
                        await _userRepository.UpdateUser(user);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
