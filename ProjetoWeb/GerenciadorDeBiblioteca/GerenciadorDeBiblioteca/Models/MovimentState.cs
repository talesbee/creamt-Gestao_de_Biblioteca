using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GerenciadorDeBiblioteca.Models
{
    public class MovimentState
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]

        public string? Name { get; set; }
        [Display(Name = "Descrição")]

        public string? Description { get; set; }
    }
}
