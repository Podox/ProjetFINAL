using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet1.Data;
using Projet1.Models;

namespace Projet1.Controllers
{
    public class FacturesController : Controller
    {
        private readonly Projet1Context _context;

        public FacturesController(Projet1Context context)
        {
            _context = context;
        }

        // GET: Factures
        public async Task<IActionResult> Index()
        {
            // Check if the current user is "admin"
            var currentUser = HttpContext.Session.GetString("Username");

            if (currentUser != "admin")
            {
                // Redirect to a different page if the user is not admin
                return RedirectToAction("AccessDenied", "Home");
            }
            return View(await _context.Facture.ToListAsync());
        }

        // GET: Factures/Details/5
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

        // GET: Factures/Create
        public IActionResult Create()
        {
            // Populate the dropdown with available Entreprises
            ViewData["EntrepriseAssocieeId"] = new SelectList(_context.Entreprise, "Id", "Nom");
            return View();
        }

        // POST: Factures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateEmission,DateEcheance,MontantTotal,EstPayee,EntrepriseAssocieeId")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the dropdown in case of validation failure
            ViewData["EntrepriseAssocieeId"] = new SelectList(_context.Entreprise, "Id", "Nom", facture.EntrepriseAssocieeId);
            return View(facture);
        }

        // GET: Factures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture.FindAsync(id);
            if (facture == null)
            {
                return NotFound();
            }

            // Populate the dropdown with available Entreprises
            ViewData["EntrepriseAssocieeId"] = new SelectList(_context.Entreprise, "Id", "Nom", facture.EntrepriseAssocieeId);
            return View(facture);
        }

        // POST: Factures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateEmission,DateEcheance,MontantTotal,EstPayee,EntrepriseAssocieeId")] Facture facture)
        {
            if (id != facture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the dropdown in case of validation failure
            ViewData["EntrepriseAssocieeId"] = new SelectList(_context.Entreprise, "Id", "Nom", facture.EntrepriseAssocieeId);
            return View(facture);
        }

        // GET: Factures/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facture = await _context.Facture.FindAsync(id);
            if (facture != null)
            {
                _context.Facture.Remove(facture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactureExists(int id)
        {
            return _context.Facture.Any(e => e.Id == id);
        }
    }
}
