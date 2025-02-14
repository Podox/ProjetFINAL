﻿using Microsoft.AspNetCore.Mvc;
using Projet1.Models;
using Microsoft.AspNetCore.Http; // For session handling
using System.Linq;
using Projet1.Data;

namespace Projet1.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly Projet1Context _context;

        public EntrepriseController(Projet1Context context)
        {
            _context = context;
        }

        // Get the page to add entreprise
        public IActionResult AddEntreprise()
        {
            // Get the current user ID from session
            var userIdString = HttpContext.Session.GetString("UserId");

            if (userIdString != null)
            {
                int userId = int.Parse(userIdString);

                // Check if the user already has an EntrepriseUId assigned
                var utilisateur = _context.Utilisateur.FirstOrDefault(u => u.Id == userId);
                if (utilisateur != null && utilisateur.EntrepriseUId != null)
                {
                    // Redirect to a page informing the user
                    return RedirectToAction("EntrepriseAssigned");
                }
            }
            else
            {
                // If user ID is not found in session, redirect to login
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddEntreprise(string Nom, string Telephone)
        {
            // Get the current user ID from session
            var userIdString = HttpContext.Session.GetString("UserId");

            if (userIdString != null)
            {
                int userId = int.Parse(userIdString);

                // Check if the user already has an EntrepriseUId assigned
                var utilisateur = _context.Utilisateur.FirstOrDefault(u => u.Id == userId);
                if (utilisateur != null && utilisateur.EntrepriseUId != null)
                {
                    // Redirect to a page informing the user
                    return RedirectToAction("EntrepriseAssigned");
                }

                // Create a new Entreprise instance and set properties
                var newEntreprise = new Entreprise
                {
                    Nom = Nom,
                    Telephone = Telephone
                };

                try
                {
                    // Save the new Entreprise to the database
                    _context.Entreprise.Add(newEntreprise);
                    _context.SaveChanges(); // This generates the ID for newEntreprise

                    // Associate the new Entreprise with the current user
                    if (utilisateur != null)
                    {
                        utilisateur.EntrepriseUId = newEntreprise.Id; // Use the generated ID
                        _context.SaveChanges(); // Save the association
                    }

                    // Set a success message to display in the view
                    ViewBag.Message = "Entreprise added successfully!";
                }
                catch (Exception ex)
                {
                    // Log the error (optional) and set an error message for the view
                    ViewBag.Error = "An error occurred while adding the entreprise. Please try again.";
                }
            }
            else
            {
                // If user ID is not found in session, set an error message
                ViewBag.Error = "User not logged in. Please log in and try again.";
            }

            // Return the same view with messages
            return View();
        }

        // Action for the page informing the user they already have an enterprise assigned
        public IActionResult EntrepriseAssigned()
        {
            ViewBag.Message = "Vous avez déjà une entreprise assignée.";
            return View();
        }
    }
}
