using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class TicketStatusDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]        
        public string Color { get; set; }
    }
}