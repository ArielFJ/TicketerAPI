using System;
using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class TicketDTO
    {        
        [Required]
        public DateTime? FechaCreacion { get; set; }    
        [Required]
        public int? UsuarioId { get; set; }                
        [Required]
        public int? ServicioId { get; set; }        
        [Required]
        public int? TicketStatusId { get; set; }        
        [Required]
        public int? PrioridadId { get; set; }        
    }
}