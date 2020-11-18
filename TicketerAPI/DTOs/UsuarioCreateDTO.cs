using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class UsuarioCreateDTO
    {        
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }        

        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Contrasena { get; set; }
        [Required]
        public int? RolId { get; set; }      
    }
}