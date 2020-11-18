using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketerAPI.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        [Required]
        public int UsuarioId { get; set; }        
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        
        [Required]
        public int ServicioId { get; set; }
        [ForeignKey("ServicioId")]
        public Servicio Servicio { get; set; }

        [Required]
        public int TicketStatusId { get; set; }
        [ForeignKey("TicketStatusId")]
        public TicketStatus TicketStatus { get; set; }

        [Required]
        public int PrioridadId { get; set; }
        [ForeignKey("PrioridadId")]
        public TicketPrioridad Prioridad { get; set; }

        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}