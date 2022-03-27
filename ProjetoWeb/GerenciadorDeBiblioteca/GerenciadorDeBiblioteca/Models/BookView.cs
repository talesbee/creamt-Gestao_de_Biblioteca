using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorDeBiblioteca.Models
{
    public class BookView
    {
       
            public int Id { get; set; }
            public int ISBN { get; set; }
            [Display(Name = "Nome")]
            public string Title { get; set; }
            [Display(Name = "Autor")]
             public string IdAuthor { get; set; }
            [Display(Name = "Editora")]
            public string PublishingCompany { get; set; }
            [Display(Name = "Edição")]
            public string Edition { get; set; }
            [Display(Name = "Ano")]
            public int Year { get; set; }
            [Display(Name = "Categoria")]

            public string IdCategory { get; set; }
            [Display(Name = "Situação")]

            public string IdState { get; set; }
        
    }
}
