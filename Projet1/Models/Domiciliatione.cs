using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet1.Models
{
    public class Domiciliatione
    {
        public int Id { get; set; }

        // Navigation properties
        public int? idUtilisateur { get; set; }
        public int? idDocument { get; set; }
        public int? idAdresseDomiciliation { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public int? etat { get; set; }


    }
}
