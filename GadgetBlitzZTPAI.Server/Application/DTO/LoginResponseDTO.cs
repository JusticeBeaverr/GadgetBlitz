namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class LoginResponseDTO
    {
        public LoginResponseDTO(Guid userId, string username, string email, string token)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Token = token;
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        
    }
}
