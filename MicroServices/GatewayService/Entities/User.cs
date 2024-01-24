using System.Text.RegularExpressions;

namespace GatewayService.Entities
{
    public class User
    {
        public User(string nom, string prenom, string email, string pass,string username)
        {
            ValidatePassword(pass);
            ValidateEmail(email);
            Id = GenerateUserId();
            Prenom = prenom;
            Nom = nom;
            Email = email;
            Pass = pass;
            UserName = username;
        }
        private string GenerateUserId()
        {
            return "user-" + Guid.NewGuid().ToString().Substring(0, 6);
        }

        private void ValidatePassword(string password)
        {
            if (password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.", nameof(password));
            }

            if (!IsAlphanumeric(password))
            {
                throw new ArgumentException("Password must be alphanumeric.", nameof(password));
            }
        }

        private bool IsAlphanumeric(string value)
        {
            var regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(value);
        }
        private void ValidateEmail(string email)
        {
            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.", nameof(email));
            }
        }
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return !string.IsNullOrEmpty(email) && regex.IsMatch(email);
        }
        public string Id { get; set; }
        public string? Prenom { get; set; }
        public string? Nom { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string NomComplet => Nom + " " + Prenom;
        public string UserName { get; set; }
    }
    public class UserLogin
    {
        public required string Email { get; set; }
        public required string Pass { get; set; }
    }
    public class UserToken
    {
        public User? User { get; set; }
        public string? Token { get; set; }
    }
}