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
    public class MovimentBookListsController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

        public MovimentBookListsController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        // GET: MovimentBookLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovimentBookList.ToListAsync());
        }

        // GET: MovimentBookLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentBookList = await _context.MovimentBookList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentBookList == null)
            {
                return NotFound();
            }

            return View(movimentBookList);
        }

        // GET: MovimentBookLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovimentBookLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdBook,IdMoviment")] MovimentBookList movimentBookList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentBookList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimentBookList);
        }

        // GET: MovimentBookLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentBookList = await _context.MovimentBookList.FindAsync(id);
            if (movimentBookList == null)
            {
                return NotFound();
            }
            return View(movimentBookList);
        }

        // POST: MovimentBookLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdBook,IdMoviment")] MovimentBookList movimentBookList)
        {
            if (id != movimentBookList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentBookList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentBookListExists(movimentBookList.Id))
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
            return View(movimentBookList);
        }

        // GET: MovimentBookLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentBookList = await _context.MovimentBookList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentBookList == null)
            {
                return NotFound();
            }

            return View(movimentBookList);
        }

        // POST: MovimentBookLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentBookList = await _context.MovimentBookList.FindAsync(id);
            _context.MovimentBookList.Remove(movimentBookList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentBookListExists(int id)
        {
            return _context.MovimentBookList.Any(e => e.Id == id);
        }
    }
}
