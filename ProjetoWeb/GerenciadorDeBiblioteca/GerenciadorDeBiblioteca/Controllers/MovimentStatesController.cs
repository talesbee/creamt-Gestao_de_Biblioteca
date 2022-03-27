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
    public class MovimentStatesController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

        public MovimentStatesController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        // GET: MovimentStates
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovimentState.ToListAsync());
        }

        // GET: MovimentStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentState = await _context.MovimentState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentState == null)
            {
                return NotFound();
            }

            return View(movimentState);
        }

        // GET: MovimentStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovimentStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] MovimentState movimentState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimentState);
        }

        // GET: MovimentStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentState = await _context.MovimentState.FindAsync(id);
            if (movimentState == null)
            {
                return NotFound();
            }
            return View(movimentState);
        }

        // POST: MovimentStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] MovimentState movimentState)
        {
            if (id != movimentState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentStateExists(movimentState.Id))
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
            return View(movimentState);
        }

        // GET: MovimentStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentState = await _context.MovimentState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentState == null)
            {
                return NotFound();
            }

            return View(movimentState);
        }

        // POST: MovimentStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentState = await _context.MovimentState.FindAsync(id);
            _context.MovimentState.Remove(movimentState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentStateExists(int id)
        {
            return _context.MovimentState.Any(e => e.Id == id);
        }
    }
}
