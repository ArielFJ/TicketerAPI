using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        [MinLength(6)]
        public string Contrasena { get; set; }
    }
}