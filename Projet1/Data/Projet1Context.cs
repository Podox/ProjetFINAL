using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projet1.Models;

namespace Projet1.Data
{
    public class Projet1Context : DbContext
    {
        public Projet1Context (DbContextOptions<Projet1Context> options)
            : base(options)
        {
        }

        public DbSet<Projet1.Models.Adresse> Adresse { get; set; } = default!;
        public DbSet<Projet1.Models.Domiciliatione> Domiciliatione { get; set; } = default!;
        public DbSet<Projet1.Models.Entreprise> Entreprise { get; set; } = default!;
        public DbSet<Projet1.Models.Facture> Facture { get; set; } = default!;
        public DbSet<Projet1.Models.Utilisateur> Utilisateur { get; set; } = default!;
    }
}
