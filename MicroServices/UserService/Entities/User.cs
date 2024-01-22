using System;

namespace UserService.Entities
{
    public class User
    {
        public User()
        {
            Id = "user-" + Guid.NewGuid().ToString().Substring(0,6);
            Prenom = "";
            Nom = "";
            Email = "";
            Pass = "";
        }

        public User(string id, string nom, string prenom, string email, string pass)
        {
            Id = "user" + Guid.NewGuid().ToString().Substring(0,6);
            Prenom = prenom;
            Nom = nom;
            Email = email;
            Pass = pass;
        }

        public string Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Pass { get; private set; }
        public string NomComplet => Nom + " " + Prenom;
    }
}
