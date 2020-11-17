using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.Models
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }
        public float Puntuacion { get; set; }        
    }
}