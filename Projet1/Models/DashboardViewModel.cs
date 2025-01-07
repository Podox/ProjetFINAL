namespace Projet1.Models
{
    public class DashboardViewModel
    {
        public Utilisateur Utilisateur { get; set; }
        public Entreprise Entreprise { get; set; }
        public List<Facture> Factures { get; set; }
        public List<Domiciliatione> Domiciliationes { get; set; }
        public List<Adresse> Adresses { get; set; }
    }
}
