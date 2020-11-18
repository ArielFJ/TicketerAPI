using System;
using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.DTOs
{
    public class ClienteDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public DateTime Nacimiento { get; set; }     
    }
}