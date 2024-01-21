using System.ComponentModel.DataAnnotations;

namespace UserService.Entities
{
    public class User(int id, string nom, string prenom, string email, string pass)
    {
        public int Id { get; set; } = id;
        public string Prenom { get; set; } = prenom;
        public string Nom { get; set; } = nom;
        public string Email { get; set; } = email;
        public string Pass { get; private set; } = pass;
        public string NomComplet => Nom + " " + Prenom;

        public void ChangePass()
        {
            this.Pass = "muchSecure";
        }
        public bool IsPassSecure()
        {
            if(Pass.Length > 6)
            {
                return true;
            }
            return false;
        }
    }
}