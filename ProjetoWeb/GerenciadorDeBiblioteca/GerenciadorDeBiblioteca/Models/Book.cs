using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GerenciadorDeBiblioteca.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        [Display(Name = "Nome")]
        public string Title { get; set; }
        [Display(Name = "Autor")]
        public int IdAuthor { get; set; }
        [Display(Name = "Editora")]
        public string PublishingCompany { get; set; }
        [Display(Name = "Edição")]
        public string Edition { get; set; }
        [Display(Name = "Ano")]
        public int  Year { get; set; }
        [Display(Name = "Categoria")]

        public int IdCategory { get; set; }
        [Display(Name = "Situação")]

        public int IdState { get; set; }
    }
}
