using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketerAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }     
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int RolId { get; set; }
        
        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
    }
}
