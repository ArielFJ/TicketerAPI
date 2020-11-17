using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class RolWriteDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}