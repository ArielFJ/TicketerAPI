using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class ServicioWriteDTO
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }
        public float Puntuacion { get; set; }      
    }
}