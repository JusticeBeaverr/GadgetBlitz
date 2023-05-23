namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class AuthenticateUserResultDTO
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
