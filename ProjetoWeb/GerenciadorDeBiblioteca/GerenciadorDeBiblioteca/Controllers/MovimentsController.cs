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
            
            foreach (var mov in moviments)
            {
                movItem.PersonName = _context.Person.Where(y => y.Id == mov.IdPerson).Select(x=>x.Name).ToString();
                movItem.ResponsibleName = _context.Person.Where(y => y.Id == mov.IdResponsible).Select(x => x.Name).ToString();
                if(mov.DateDeadline == null)
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
                var movBooks = _context.MovimentBookList.Where(x => x.IdMoviment == mov.Id).Select(y => y.IdBook);
                foreach(var book in movBooks)
                {
                    MovimentBooks bookItem = new();
                    bookItem.BookName = _context.Book.Where(z => z.Id == Convert.ToInt16(book)).Select(x => x.Title).ToString();
                    movItem.Books.Add(bookItem);
                }
                
                movimentsView.Add(movItem);
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
            movItem.PersonName = _context.Person.Where(y => y.Id == moviment.IdPerson).Select(x => x.Name).ToString();
            movItem.ResponsibleName = _context.Person.Where(y => y.Id == moviment.IdResponsible).Select(x => x.Name).ToString();
            if (moviment.DateDeadline == null)
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
            var movBooks = _context.MovimentBookList.Where(x => x.IdMoviment == moviment.Id).Select(y => y.IdBook);
            foreach (var book in movBooks)
            {
                MovimentBooks bookItem = new();
                bookItem.BookName = _context.Book.Where(z => z.Id == Convert.ToInt16(book)).Select(x => x.Title).ToString();
                movItem.Books.Add(bookItem);
            }
            return View(movItem);
        }
        public class PeopleModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class BookModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        // GET: Moviments/Create
        public IActionResult Create()
        {
            List<PeopleModel> dataPeople = new List<PeopleModel>();
            List<BookModel> dataBooks = new List<BookModel>();
            var peoples = _context.Person.ToList();
            var books = _context.Book.ToList();
            foreach (var person in peoples)
            {
                dataPeople.Add(new PeopleModel() { Id = person.Id, Name = person.Name });
            }
            foreach (var book in books)
            {
                dataBooks.Add(new BookModel() { Id = book.Id, Name = book.Title });
            }

            ViewBag.People = _context.Person.ToList();
            ViewBag.Books = dataBooks;
            return View();
        }

        // POST: Moviments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdBook,IdPerson,IdResponsible,DateMoviment,DateDeadline,DateMaxDeadline,IdState")] Moviment moviment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moviment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moviment);
        }

        // GET: Moviments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdBook,IdPerson,IdResponsible,DateMoviment,DateDeadline,DateMaxDeadline,IdState")] Moviment moviment)
        {
            if (id != moviment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moviment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentExists(moviment.Id))
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
