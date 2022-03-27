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
    public class BooksController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

        public BooksController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }






        // GET: Books
        public async Task<IActionResult> Index()
        {
            BookView bookItem;
            List<BookView> booklist = new ();
            var books = await _context.Book.ToListAsync();
            if (books.Any())
            {
                foreach (var book in books)
                {
                    bookItem = new();
                    bookItem.Id = book.Id;
                    bookItem.Title = book.Title;
                    bookItem.IdCategory = _context.Category.Where(x => x.Id == book.IdCategory).FirstOrDefault().Name;
                    bookItem.Year = book.Year;
                    bookItem.ISBN = book.ISBN;
                    bookItem.Edition = book.Edition;
                    bookItem.IdAuthor = _context.Person.Where(x => x.Id == book.IdAuthor).FirstOrDefault().Name;
                    bookItem.IdState = _context.BookState.Where(x => x.Id == book.IdState).FirstOrDefault().Name;
                    bookItem.PublishingCompany = book.PublishingCompany;
                    booklist.Add(bookItem);
                }
            }
            return View(booklist);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            BookView bookItem;
            
            bookItem = new();
            bookItem.Id = book.Id;
            bookItem.Title = book.Title;
            bookItem.IdCategory = _context.Category.Where(x => x.Id == book.IdCategory).FirstOrDefault().Name;
            bookItem.Year = book.Year;
            bookItem.ISBN = book.ISBN;
            bookItem.Edition = book.Edition;
            bookItem.IdAuthor = _context.Person.Where(x => x.Id == book.IdAuthor).FirstOrDefault().Name;
            bookItem.IdState = _context.BookState.Where(x => x.Id == book.IdState).FirstOrDefault().Name;
            bookItem.PublishingCompany = book.PublishingCompany;

               

            return View(bookItem);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.People = _context.Person.ToList();
            ViewBag.Category = _context.Category.ToList();
            ViewBag.Sate = _context.BookState.ToList();
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ISBN,Title,IdAuthor,PublishingCompany,Edition,Year,IdCategory,IdState")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.People = _context.Person.ToList();
            ViewBag.Category = _context.Category.ToList();
            ViewBag.Sate = _context.BookState.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,Title,IdAuthor,PublishingCompany,Edition,Year,IdCategory,IdState")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            BookView bookItem;

            bookItem = new();
            bookItem.Id = book.Id;
            bookItem.Title = book.Title;
            bookItem.IdCategory = _context.Category.Where(x => x.Id == book.IdCategory).FirstOrDefault().Name;
            bookItem.Year = book.Year;
            bookItem.ISBN = book.ISBN;
            bookItem.Edition = book.Edition;
            bookItem.IdAuthor = _context.Person.Where(x => x.Id == book.IdAuthor).FirstOrDefault().Name;
            bookItem.IdState = _context.BookState.Where(x => x.Id == book.IdState).FirstOrDefault().Name;
            bookItem.PublishingCompany = book.PublishingCompany;
            return View(bookItem);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
