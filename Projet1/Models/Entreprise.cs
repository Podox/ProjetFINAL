using System.ComponentModel.DataAnnotations.Schema;

namespace Projet1.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Telephone { get; set; }

    }
}
