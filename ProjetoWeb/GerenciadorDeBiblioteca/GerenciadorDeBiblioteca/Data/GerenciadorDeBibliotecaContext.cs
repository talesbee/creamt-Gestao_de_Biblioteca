#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeBiblioteca.Models;

namespace GerenciadorDeBiblioteca.Data
{
    public class GerenciadorDeBibliotecaContext : DbContext
    {
        public GerenciadorDeBibliotecaContext (DbContextOptions<GerenciadorDeBibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<GerenciadorDeBiblioteca.Models.Book> Book { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.BookState> BookState { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.Category> Category { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.Moviment> Moviment { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.MovimentBookList> MovimentBookList { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.MovimentState> MovimentState { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.Person> Person { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.TipePerson> TipePerson { get; set; }

        public DbSet<GerenciadorDeBiblioteca.Models.User> User { get; set; }
    }
}
