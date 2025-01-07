using Microsoft.AspNetCore.Mvc;
using Projet1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Projet1.Data;

namespace Projet1.Controllers
{
    public class FactureController : Controller
    {
        private readonly Projet1Context _context;

        public FactureController(Projet1Context context)
        {
            _context = context;
        }

        // GET: Facture/Create
        public IActionResult Create()
        {
            // Get a list of Entreprises for the dropdown
            var entreprises = _context.Entreprise.ToList();
            ViewBag.Entreprises = entreprises;

            return View();
        }

        // POST: Facture/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateEmission,DateEcheance,MontantTotal,EstPayee,EntrepriseAssocieeId")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                // Add the new Facture to the database
                _context.Add(facture);
                await _context.SaveChangesAsync();

                // If Entreprise is associated, update the state of Domiciliatione
                if (facture.EntrepriseAssocieeId != null)
                {
                    var utilisateur = await _context.Utilisateur
                        .Where(u => u.EntrepriseUId == facture.EntrepriseAssocieeId)
                        .FirstOrDefaultAsync();

                    if (utilisateur != null)
                    {
                        var domiciliation = await _context.Domiciliatione
                            .Where(d => d.idUtilisateur == utilisateur.Id)
                            .FirstOrDefaultAsync();

                        if (domiciliation != null)
                        {
                            // Change the etat of Domiciliatione
                            domiciliation.etat = 1; // Assuming 1 represents the updated state
                            _context.Update(domiciliation);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction(nameof(Index)); // Redirect to an Index or relevant page
            }
            return View(facture);
        }

        // GET: Facture/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // GET: Facture/Index
        public async Task<IActionResult> Index()
        {
            var factures = await _context.Facture
                .Include(f => f.EntrepriseAssocieeId)
                .ToListAsync();
            return View(factures);
        }
    }
}
