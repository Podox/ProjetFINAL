using Microsoft.AspNetCore.Mvc;
using Projet1.Data;
using Projet1.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projet1.Controllers
{
    public class DispAdresseController : Controller
    {
        private readonly Projet1Context _context;

        public DispAdresseController(Projet1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var adresses = _context.Adresse.ToList();

            var viewModel = adresses.Select(a => new
            {
                a.Id,
                a.Rue,
                a.Ville,
                a.CodePostal,
                a.Pays,
                Etat = a.etat == 1 ? "dispo" : "pas dispo"
            }).ToList();

            return View(viewModel);
        }
    }
}