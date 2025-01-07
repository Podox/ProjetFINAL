using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet1.Data;
using Projet1.Models;

namespace Projet1.Controllers
{
    public class DomiciliationesController : Controller
    {
        private readonly Projet1Context _context;

        public DomiciliationesController(Projet1Context context)
        {
            _context = context;
        }

        // GET: Domiciliationes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Domiciliatione.ToListAsync());
        }

        // GET: Domiciliationes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domiciliatione = await _context.Domiciliatione
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domiciliatione == null)
            {
                return NotFound();
            }

            return View(domiciliatione);
        }

        // GET: Domiciliationes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Domiciliationes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,idUtilisateur,idDocument,idAdresseDomiciliation,DateDebut,DateFin,etat")] Domiciliatione domiciliatione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(domiciliatione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(domiciliatione);
        }

        // GET: Domiciliationes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domiciliatione = await _context.Domiciliatione.FindAsync(id);
            if (domiciliatione == null)
            {
                return NotFound();
            }
            return View(domiciliatione);
        }

        // POST: Domiciliationes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,idUtilisateur,idDocument,idAdresseDomiciliation,DateDebut,DateFin,etat")] Domiciliatione domiciliatione)
        {
            if (id != domiciliatione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domiciliatione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomiciliationeExists(domiciliatione.Id))
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
            return View(domiciliatione);
        }

        // GET: Domiciliationes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domiciliatione = await _context.Domiciliatione
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domiciliatione == null)
            {
                return NotFound();
            }

            return View(domiciliatione);
        }

        // POST: Domiciliationes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domiciliatione = await _context.Domiciliatione.FindAsync(id);
            if (domiciliatione != null)
            {
                _context.Domiciliatione.Remove(domiciliatione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomiciliationeExists(int id)
        {
            return _context.Domiciliatione.Any(e => e.Id == id);
        }
    }
}
