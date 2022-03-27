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
    public class BookStatesController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

        public BookStatesController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        // GET: BookStates
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookState.ToListAsync());
        }

        // GET: BookStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookState = await _context.BookState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookState == null)
            {
                return NotFound();
            }

            return View(bookState);
        }

        // GET: BookStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] BookState bookState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookState);
        }

        // GET: BookStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookState = await _context.BookState.FindAsync(id);
            if (bookState == null)
            {
                return NotFound();
            }
            return View(bookState);
        }

        // POST: BookStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] BookState bookState)
        {
            if (id != bookState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookStateExists(bookState.Id))
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
            return View(bookState);
        }

        // GET: BookStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookState = await _context.BookState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookState == null)
            {
                return NotFound();
            }

            return View(bookState);
        }

        // POST: BookStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookState = await _context.BookState.FindAsync(id);
            _context.BookState.Remove(bookState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookStateExists(int id)
        {
            return _context.BookState.Any(e => e.Id == id);
        }
    }
}
