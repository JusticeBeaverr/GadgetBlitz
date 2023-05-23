namespace GadgetBlitzZTPAI.Server.Core.Entities
{
    public class AuthenticateUserResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        

        public AuthenticateUserResult(bool success, string token, string errorMessage = null)
        {
            Success = success;
            Token = token;
            ErrorMessage = errorMessage;
        }
    }
}
