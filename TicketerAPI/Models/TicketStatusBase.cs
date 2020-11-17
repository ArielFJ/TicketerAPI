using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.Models
{
    public abstract class TicketStatusBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]        
        [MaxLength(7)]        
        public string Color { get; set; } // Color hexadecimal #aabbcc
    }
}