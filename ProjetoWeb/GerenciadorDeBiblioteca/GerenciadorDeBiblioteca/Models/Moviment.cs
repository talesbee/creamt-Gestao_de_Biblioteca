using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeBiblioteca.Models
{
    public class Moviment
    {
        public int Id { get; set; }
        public int IdPerson { get; set; }
        public int IdResponsible { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateMoviment { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateDeadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateMaxDeadline { get; set; }
        public int IdState { get; set; } = 1;

    }
}
