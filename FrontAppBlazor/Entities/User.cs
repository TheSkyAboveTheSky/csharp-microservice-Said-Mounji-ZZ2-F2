using System.Text.RegularExpressions;

namespace FrontAppBlazor.Entities
{
    public class User
    {
        public User(string nom, string prenom, string email, string password, string username, string gender, int groupId = 0,string role = "User")
        {
            ValidatePassword(password);
            ValidateEmail(email);
            Id = GenerateUserId();
            Prenom = prenom;
            Nom = nom;
            Email = email;
            Password = password;
            Username = username;
            Gender = gender;
            Role = role;
            GroupId = groupId;
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
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public int GroupId { get; set; }

    }
    public class UserLogin
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class UserToken
    {
        public User? User { get; set; }
        public string? Token { get; set; }
    }
    public class UserModelUpdate
    {
        public string? Prenom { get; set; }
        public string? Nom { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public int? GroupId { get; set; }

    }
}