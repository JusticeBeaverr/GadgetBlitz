using GadgetBlitzZTPAI.Server.Core.Entities;
using System.Security.Claims;

namespace GadgetBlitzZTPAI.Server.Core
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
