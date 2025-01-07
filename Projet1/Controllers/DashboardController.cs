using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet1.Data;
using Projet1.Models;
using System.Linq;

namespace Projet1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Projet1Context _context;

        public DashboardController(Projet1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve the UserId from the session
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if no user session
            }

            int userId = int.Parse(userIdString);

            // Retrieve the user based on the UserId
            var utilisateur = _context.Utilisateur
                .FirstOrDefault(u => u.Id == userId);

            if (utilisateur == null)
            {
                return NotFound();
            }

            // Retrieve the associated Entreprise using the EntrepriseUId in Utilisateur
            var entreprise = _context.Entreprise
                .FirstOrDefault(e => e.Id == utilisateur.EntrepriseUId);

            // Retrieve all Factures for this EntrepriseAssocieeId
            var factures = _context.Facture
                .Where(f => f.EntrepriseAssocieeId == utilisateur.EntrepriseUId)
                .ToList();

            // Retrieve all Domiciliationes for this Utilisateur
            var domiciliationes = _context.Domiciliatione
                .Where(d => d.idUtilisateur == utilisateur.Id)
                .ToList();

            // Retrieve associated Adresses using idAdresseDomiciliation from domiciliationes
            var adresseIds = domiciliationes.Select(d => d.idAdresseDomiciliation).ToList();
            var adresses = _context.Adresse
                .Where(a => adresseIds.Contains(a.Id))
                .ToList();

            // Create a ViewModel to pass all this data to the view
            var dashboardViewModel = new DashboardViewModel
            {
                Utilisateur = utilisateur,
                Entreprise = entreprise,
                Factures = factures,
                Domiciliationes = domiciliationes,
                Adresses = adresses
            };

            return View(dashboardViewModel);
        }
    }
}
