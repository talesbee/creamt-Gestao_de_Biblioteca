#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeBiblioteca.Data;
using GerenciadorDeBiblioteca.Models;

namespace GerenciadorDeBiblioteca.Controllers
{
    public class TipePersonsController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

        public TipePersonsController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        // GET: TipePersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipePerson.ToListAsync());
        }

        // GET: TipePersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipePerson = await _context.TipePerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipePerson == null)
            {
                return NotFound();
            }

            return View(tipePerson);
        }

        // GET: TipePersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipePersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DeadlineDays")] TipePerson tipePerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipePerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipePerson);
        }

        // GET: TipePersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipePerson = await _context.TipePerson.FindAsync(id);
            if (tipePerson == null)
            {
                return NotFound();
            }
            return View(tipePerson);
        }

        // POST: TipePersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DeadlineDays")] TipePerson tipePerson)
        {
            if (id != tipePerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipePerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipePersonExists(tipePerson.Id))
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
            return View(tipePerson);
        }

        // GET: TipePersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipePerson = await _context.TipePerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipePerson == null)
            {
                return NotFound();
            }

            return View(tipePerson);
        }

        // POST: TipePersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipePerson = await _context.TipePerson.FindAsync(id);
            _context.TipePerson.Remove(tipePerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipePersonExists(int id)
        {
            return _context.TipePerson.Any(e => e.Id == id);
        }
    }
}
