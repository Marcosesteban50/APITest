using System.ComponentModel.DataAnnotations;

namespace APIPruebas.DTOs
{
    public class CredencialesUsuario
    {

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
