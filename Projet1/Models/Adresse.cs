namespace Projet1.Models
{
    public class Adresse
    {
        public int Id { get; set; }
        public string? Rue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }
        public string? Pays { get; set; }
        public int? etat { get; set; }
    }

}