using System.ComponentModel.DataAnnotations.Schema;

namespace Projet1.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public DateTime? DateEmission { get; set; }
        public DateTime? DateEcheance { get; set; } // Optionnel pour les paiements à terme
        public decimal? MontantTotal { get; set; }
        public bool? EstPayee { get; set; } // Pour indiquer si la facture est réglée
  
        public int? EntrepriseAssocieeId { get; set; }

    }
}
