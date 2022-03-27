namespace GerenciadorDeBiblioteca.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public bool CellPhoneWhatsApp { get; set; }
        public int IdTipePerson { get; set; }
        public bool State { get; set; }
    }
}
