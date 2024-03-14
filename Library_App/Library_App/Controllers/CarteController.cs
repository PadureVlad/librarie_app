using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_App.Models;

namespace Library_App.Controllers
{
    public class CarteController : Controller
    {
        private readonly CarteContext _context;

        public CarteController(CarteContext context)
        {
            _context = context;
        }

        // GET: Carte
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carti.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> FiltrarePret()
        {
            var cartiFiltrate = _context.GetCartiPret();
            return View("Index", await cartiFiltrate.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> FiltrareAutor()
        {
            var cartiFiltrate = _context.GetAutorConsoana();
            return View("Index", await cartiFiltrate.ToListAsync());
        }
        [HttpPost]
        public IActionResult FiltrarePretMaximal()
        {
            var cartePretMaximal = _context.Carti.OrderByDescending(carte => carte.Pret).FirstOrDefault();
            return View("Index", new List<Carte> { cartePretMaximal });
        }

        // GET: Carte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Carti
                .FirstOrDefaultAsync(m => m.CodCarte == id);
            if (carte == null)
            {
                return NotFound();
            }

            return View(carte);
        }

        // GET: Carte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCarte,Titlu,Autor,Editor,Pret,Imagine")] Carte carte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carte);
        }

        // GET: Carte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Carti.FindAsync(id);
            if (carte == null)
            {
                return NotFound();
            }
            return View(carte);
        }

        // POST: Carte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodCarte,Titlu,Autor,Editor,Pret,Imagine")] Carte carte)
        {
            if (id != carte.CodCarte)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarteExists(carte.CodCarte))
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
            return View(carte);
        }

        // GET: Carte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Carti
                .FirstOrDefaultAsync(m => m.CodCarte == id);
            if (carte == null)
            {
                return NotFound();
            }

            return View(carte);
        }

        // POST: Carte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carte = await _context.Carti.FindAsync(id);
            if (carte != null)
            {
                _context.Carti.Remove(carte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarteExists(int id)
        {
            return _context.Carti.Any(e => e.CodCarte == id);
        }
    }
}
