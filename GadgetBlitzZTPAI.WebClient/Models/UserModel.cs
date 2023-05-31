namespace GadgetBlitzZTPAI.WebClient.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set;}
        public string Email {get;set;}
        public string Password { get; set;}
        public string Role { get; set;}
        public string Token { get; set;}

        
    }
}
