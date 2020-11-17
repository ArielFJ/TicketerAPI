using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
