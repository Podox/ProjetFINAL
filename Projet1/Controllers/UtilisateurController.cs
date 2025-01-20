using Microsoft.AspNetCore.Mvc;
using Projet1.Data;
using Projet1.Models;
using System;
using System.Linq;

namespace Projet1.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly Projet1Context _context;

        public UtilisateurController(Projet1Context context)
        {
            _context = context;
        }

        public IActionResult Edit()
        {
            // Retrieve the current user ID from the session
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            int id = int.Parse(userId);
            var utilisateur = _context.Utilisateur.FirstOrDefault(u => u.Id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        [HttpPost]
        public IActionResult Edit(Utilisateur updatedUtilisateur)
        {
            // Retrieve the current user ID from the session
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            int id = int.Parse(userId);
            var utilisateur = _context.Utilisateur.FirstOrDefault(u => u.Id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            // Update the user details except for EntrepriseUId
            utilisateur.Nom = updatedUtilisateur.Nom;
            utilisateur.Prenom = updatedUtilisateur.Prenom;
            utilisateur.Email = updatedUtilisateur.Email;
            utilisateur.MotDePasse = updatedUtilisateur.MotDePasse;
            utilisateur.Telephone = updatedUtilisateur.Telephone;

            _context.SaveChanges();

            return RedirectToAction("Profile", "Utilisateur");
        }

        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            int id = int.Parse(userId);
            var utilisateur = _context.Utilisateur.FirstOrDefault(u => u.Id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }
    }
}
