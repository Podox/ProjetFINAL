using Microsoft.AspNetCore.Mvc;
using Projet1.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Projet1.Data;

namespace Projet1.Controllers
{
    public class DomiciliationeController : Controller
    {
        private readonly Projet1Context _context;

        public DomiciliationeController(Projet1Context context)
        {
            _context = context;
        }

        // GET: Show the Add Domiciliatione page
        public IActionResult AddDomiciliatione()
        {
            // Fetch addresses with etat = 1 to populate the dropdown
            var availableAddresses = _context.Adresse.Where(a => a.etat == 1).ToList();
            ViewBag.AvailableAddresses = availableAddresses;

            return View();
        }

        [HttpPost]
        public IActionResult AddDomiciliatione(int idAdresseDomiciliation, DateTime? DateDebut, DateTime? DateFin)
        {
            // Get the current user ID from session
            var userIdString = HttpContext.Session.GetString("UserId");

            if (userIdString != null)
            {
                int userId = int.Parse(userIdString);

                try
                {
                    // Create a new Domiciliatione instance
                    var newDomiciliatione = new Domiciliatione
                    {
                        idUtilisateur = userId,
                        idAdresseDomiciliation = idAdresseDomiciliation,
                        idDocument = null, // Leave null as requested
                        DateDebut = DateDebut,
                        DateFin = DateFin
                    };

                    // Save the new Domiciliatione to the database
                    _context.Domiciliatione.Add(newDomiciliatione);
                    _context.SaveChanges();

                    // Update the etat of the chosen address to 0
                    var chosenAddress = _context.Adresse.FirstOrDefault(a => a.Id == idAdresseDomiciliation);
                    if (chosenAddress != null)
                    {
                        chosenAddress.etat = 0;
                        _context.SaveChanges(); // Save the updated address
                    }

                    // Set a success message to display in the view
                    ViewBag.Message = "Domiciliatione added successfully, and address status updated!";
                }
                catch (Exception ex)
                {
                    // Log the error (optional) and set an error message for the view
                    ViewBag.Error = "An error occurred while adding the domiciliatione. Please try again.";
                }
            }
            else
            {
                // If user ID is not found in session, set an error message
                ViewBag.Error = "User not logged in. Please log in and try again.";
            }

            // Fetch addresses again for the dropdown menu
            var availableAddresses = _context.Adresse.Where(a => a.etat == 1).ToList();
            ViewBag.AvailableAddresses = availableAddresses;

            // Return the same view
            return View();
        }
    }
}
