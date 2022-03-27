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
    public class MovimentsController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

        public MovimentsController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Moviments
        public async Task<IActionResult> Index()
        {
            var moviments = await _context.Moviment.ToListAsync();
            
            List<MovimentView> movimentsView = new ();
            MovimentView movItem = new ();
            if (moviments.Any())
            {
                foreach (var mov in moviments)
                {
                    movItem.Id = mov.Id;
                    movItem.PersonName = _context.Person.Where(y => y.Id == mov.IdPerson).FirstOrDefault().Name;
                    movItem.ResponsibleName = _context.Person.Where(y => y.Id == mov.IdResponsible).FirstOrDefault().Name;
                    movItem.DataMoviment = mov.DateMoviment;
                    if (mov.DateDeadline == null || mov.DateDeadline == new DateTime())
                    {
                        movItem.DateDeadline = mov.DateMaxDeadline;
                        if (mov.DateMoviment > mov.DateMaxDeadline)
                        {
                            movItem.Status = "Atrasado";

                        }
                        else
                        {
                            movItem.Status = "Emprestado";
                        }
                    }
                    else
                    {
                        movItem.DateDeadline = mov.DateDeadline;
                        movItem.Status = "Devolvido";
                    }

                    movimentsView.Add(movItem);
                }
            }
            return View(movimentsView);
        }

        // GET: Moviments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviment = await _context.Moviment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviment == null)
            {
                return NotFound();
            }
            MovimentView movItem = new();
            movItem.Id = moviment.Id;
            movItem.PersonName = _context.Person.Where(y => y.Id == moviment.IdPerson).FirstOrDefault().Name;
            movItem.ResponsibleName = _context.Person.Where(y => y.Id == moviment.IdResponsible).FirstOrDefault().Name;
            movItem.DataMoviment = moviment.DateMoviment;
            movItem.Book = _context.Book.Where(y => y.Id == moviment.IdBook).FirstOrDefault().Title;
            if (moviment.DateDeadline == null || moviment.DateDeadline == new DateTime())
            {
                movItem.DateDeadline = moviment.DateMaxDeadline;
                if (moviment.DateMoviment > moviment.DateMaxDeadline)
                {
                    movItem.Status = "Atrasado";
                }
                else
                {
                    movItem.Status = "Emprestado";
                }
            }
            else
            {
                movItem.DateDeadline = moviment.DateDeadline;
                movItem.Status = "Devolvido";
            }

            return View(movItem);
        }

        // GET: Moviments/Create
        public IActionResult Create()
        {
            ViewBag.People = _context.Person.ToList();
            ViewBag.Books = _context.Book.ToList();
            return View();
        }


        public class movimentReturn
        {
            public int IdPerson { get; set; }
            public int IdResponsible { get; set; }
            public int IdBook { get; set; }
            public DateTime DateMoviment { get; set; }
            public DateTime DateDeadline { get; set; }
        }


        // POST: Moviments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPerson,IdResponsible,IdBook,DateMoviment")] movimentReturn moviment)
        {
            Moviment movItem = new();
            if (ModelState.IsValid)
            {
                var tipe = _context.Person.Where(x => x.Id == moviment.IdPerson).FirstOrDefault().IdTipePerson;   
                var days = _context.TipePerson.Where(c=>c.Id == tipe).FirstOrDefault().DeadlineDays;
                DateTime maxDeadline = moviment.DateMoviment.AddDays(days);

                movItem.IdPerson = moviment.IdPerson;
                movItem.IdResponsible = moviment.IdResponsible;
                movItem.DateMoviment = moviment.DateMoviment;
                movItem.DateMaxDeadline = maxDeadline;
                movItem.IdState = 1;
                movItem.IdBook = moviment.IdBook;

                _context.Add(movItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moviment);
        }

        // GET: Moviments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.People = _context.Person.ToList();
            ViewBag.Books = _context.Book.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var moviment = await _context.Moviment.FindAsync(id);
            if (moviment == null)
            {
                return NotFound();
            }
            return View(moviment);
        }

        // POST: Moviments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPerson,IdResponsible,IdBook,DateMoviment,DateDeadline")] Moviment moviment)
        {
            Moviment movItem = new();
            if (ModelState.IsValid)
            {
                try
                {
                    var tipe = _context.Person.Where(x => x.Id == moviment.IdPerson).FirstOrDefault().IdTipePerson;
                    var days = _context.TipePerson.Where(c => c.Id == tipe).FirstOrDefault().DeadlineDays;
                    DateTime maxDeadline = moviment.DateMoviment.AddDays(days);
                    movItem.Id = id;
                    movItem.IdPerson = moviment.IdPerson;
                    movItem.IdResponsible = moviment.IdResponsible;
                    movItem.DateMoviment = moviment.DateMoviment;
                    movItem.DateMaxDeadline = maxDeadline;
                    movItem.DateDeadline = moviment.DateDeadline == null ? maxDeadline : moviment.DateDeadline;
                    movItem.IdState = 1;
                    movItem.IdBook = moviment.IdBook;
                    _context.Update(movItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentExists(movItem.Id))
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
            return View(moviment);
        }

        // GET: Moviments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviment = await _context.Moviment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviment == null)
            {
                return NotFound();
            }

            return View(moviment);
        }

        // POST: Moviments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviment = await _context.Moviment.FindAsync(id);
            _context.Moviment.Remove(moviment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentExists(int id)
        {
            return _context.Moviment.Any(e => e.Id == id);
        }
    }
}
