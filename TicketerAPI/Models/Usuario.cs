using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketerAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        public string NombreUsuario { get => $"{Nombre.ToLower()}{Apellido}_{Id}"; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Contrasena { get; set; }
        [Required]
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
    }
}
