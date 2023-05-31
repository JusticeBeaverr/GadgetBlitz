namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class LoginResponseDTO
    {
        public LoginResponseDTO(Guid userId, string username, string email,string role, string token)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Role = role;
            Token = token;
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        
    }
}
