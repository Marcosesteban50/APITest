using System.ComponentModel.DataAnnotations;

namespace APIPruebas.Models
{
    public class Credencial
    {
        [Required]

        public string? Usuario { get; set; }
        [Required]

        public string? clave { get; set; }
    }
}
