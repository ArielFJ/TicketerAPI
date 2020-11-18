using System;
using System.ComponentModel.DataAnnotations;

namespace TicketerAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public DateTime Nacimiento { get; set; }        
    }
}