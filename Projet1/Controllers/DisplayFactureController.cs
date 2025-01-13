

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Projet1.Models;
using Projet1.Data;

namespace Projet1.Controllers
{
    public class DisplayFactureController : Controller
    {
        private readonly Projet1Context _context;

        public DisplayFactureController(Projet1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve the current user's UserId from session
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return View("Error", "Utilisateur non authentifié ou session invalide.");
            }

            // Fetch the current user from the database
            var utilisateur = _context.Utilisateur.FirstOrDefault(u => u.Id == userId);
            if (utilisateur == null || utilisateur.EntrepriseUId == null)
            {
                return View("Error", "Utilisateur ou entreprise non trouvé.");
            }

            // Get the user's entreprise ID
            var entrepriseId = utilisateur.EntrepriseUId;

            // Get factures associated with the entreprise
            var factures = _context.Facture
                .Where(f => f.EntrepriseAssocieeId == entrepriseId)
                .ToList();

            // Pass factures to the view
            return View(factures);
        }
    }
}