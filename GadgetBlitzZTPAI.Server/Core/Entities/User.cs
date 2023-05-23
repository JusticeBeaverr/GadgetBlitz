using Microsoft.AspNetCore.Identity;

namespace GadgetBlitzZTPAI.Server.Core.Entities
{
    public class User : AggregateRoot
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        
        

        public User(string username,string email, string password)
        {
            UserId = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = password;
            
        }

        public bool VerifyPassword(string password)
        {
            // Porównanie hasha z podanym hasłem
            var passwordVerificationResult = VerifyHashedPassword(PasswordHash, password);
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }

        private string HashPassword(string password)
        {
            // Wygenerowanie hasha dla hasła
            var passwordHash = HashPassword(password);
            return passwordHash;
        }

        private PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password)
        {
            // Weryfikacja hasła z haszem
            var passwordVerificationResult = VerifyHashedPassword(hashedPassword, password);
            return passwordVerificationResult;
        }

    }
}
