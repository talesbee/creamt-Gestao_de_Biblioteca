using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeBiblioteca.Models
{
    public class MovimentView
    {
        public int Id { get; set; }
        [Display(Name = "Pessoa")]
        public string PersonName { get; set; }
        [Display(Name = "Responsável")]
        public string ResponsibleName { get; set; }
        [Display(Name = "Data Empréstimo")]
        public DateTime DataMoviment { get; set; }
        [Display(Name = "Data Devolução")]
        public DateTime? DateDeadline { get; set; }
        [Display(Name = "Situação")]
        public string Status { get; set; }
        public string Book { get; set; }

    }

}
