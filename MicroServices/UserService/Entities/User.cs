using System;
using System.Text.RegularExpressions;

namespace UserService.Entities
{
    public class User
    {
        public User(string nom, string prenom, string email, string pass)
        {
            ValidatePassword(pass);
            ValidateEmail(email);
            Id = GenerateUserId();
            Prenom = prenom;
            Nom = nom;
            Email = email;
            Pass = pass;
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
        public string Pass { get; private set; }
        public string NomComplet => Nom + " " + Prenom;
    }
}