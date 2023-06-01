namespace GadgetBlitzZTPAI.WebClient.Models
{
    public class AddReviewCommand
    {
        public Guid SmartphoneId { get; set; }
        public string Username { get; set; }
        public string ReviewText { get; set; }
    }
}
