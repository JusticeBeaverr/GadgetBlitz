namespace GadgetBlitzZTPAI.WebClient.Models
{
    public class ChangePasswordCommand
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; } 
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
