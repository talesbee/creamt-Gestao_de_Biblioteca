using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeBiblioteca.Models
{
    public class Moviment
    {
        public int Id { get; set; }
        [Display(Name = "Pessoa")]
        public int IdPerson { get; set; }
        [Display(Name = "Livro")]
        public int IdBook { get; set; }
        [Display(Name = "Responsável")]
        public int IdResponsible { get; set; }
        [Display(Name = "Data Emprestimo")]
        [DataType(DataType.Date)]
        public DateTime DateMoviment { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateDeadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateMaxDeadline { get; set; }
        public int IdState { get; set; } = 1;

    }
}
