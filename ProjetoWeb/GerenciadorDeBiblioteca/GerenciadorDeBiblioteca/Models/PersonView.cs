using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GerenciadorDeBiblioteca.Models
{
    public class PersonView
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]

        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        [Display(Name = "Endereço")]

        public string Address { get; set; }
        public string Email { get; set; }
        [Display(Name = "Telefone")]

        public string Phone { get; set; }
        [Display(Name = "Celular")]

        public string CellPhone { get; set; }
        [Display(Name = "Whatsapp")]

        public bool CellPhoneWhatsApp { get; set; }
        [Display(Name = "Tipo")]

        public string IdTipePerson { get; set; }
        [Display(Name = "Situação")]

        public bool State { get; set; }
    }
}
