using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeBiblioteca.Data;
using GerenciadorDeBiblioteca.Models;
using Newtonsoft.Json;

namespace GerenciadorDeBiblioteca.Controllers
{
    public class PeopleController : Controller
    {
        private readonly GerenciadorDeBibliotecaContext _context;

   
 
        public PeopleController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            PersonView personItem;
            List<PersonView> personList = new();
            var peoples = await _context.Person.ToListAsync();
            if (peoples.Any())
            {
                foreach (var person in peoples)
                {
                    personItem = new();
                    personItem.Id = person.Id;
                    personItem.Name = person.Name;
                    personItem.CPF = person.CPF;
                    personItem.RG = person.RG;
                    personItem.Address = person.Address;
                    personItem.Email = person.Email;
                    personItem.Phone = person.Phone;
                    personItem.CellPhone = person.CellPhone;
                    personItem.CellPhoneWhatsApp = person.CellPhoneWhatsApp;
                    personItem.State = person.State;
                    personItem.IdTipePerson = _context.TipePerson.Where(x => x.Id == person.IdTipePerson).FirstOrDefault().Name;
                    personList.Add(personItem);
                }
            }
            return View(personList);
            
        }

 
        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            PersonView personItem;
            personItem = new();
            personItem.Id = person.Id;
            personItem.Name = person.Name;
            personItem.CPF = person.CPF;
            personItem.RG = person.RG;
            personItem.Address = person.Address;
            personItem.Email = person.Email;
            personItem.Phone = person.Phone;
            personItem.CellPhone = person.CellPhone;
            personItem.CellPhoneWhatsApp = person.CellPhoneWhatsApp;
            personItem.State = person.State;
            personItem.IdTipePerson = _context.TipePerson.Where(x => x.Id == person.IdTipePerson).FirstOrDefault().Name;
            return View(personItem);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewBag.Tipe = _context.TipePerson.ToList();
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CPF,RG,Address,Email,Phone,CellPhone,CellPhoneWhatsApp,IdTipePerson,State")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Tipe = _context.TipePerson.ToList();
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CPF,RG,Address,Email,Phone,CellPhone,CellPhoneWhatsApp,IdTipePerson,State")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            PersonView personItem;
            personItem = new();
            personItem.Id = person.Id;
            personItem.Name = person.Name;
            personItem.CPF = person.CPF;
            personItem.RG = person.RG;
            personItem.Address = person.Address;
            personItem.Email = person.Email;
            personItem.Phone = person.Phone;
            personItem.CellPhone = person.CellPhone;
            personItem.CellPhoneWhatsApp = person.CellPhoneWhatsApp;
            personItem.State = person.State;
            personItem.IdTipePerson = _context.TipePerson.Where(x => x.Id == person.IdTipePerson).FirstOrDefault().Name;
            return View(personItem);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
