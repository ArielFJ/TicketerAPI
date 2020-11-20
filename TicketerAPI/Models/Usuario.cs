using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace TicketerAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        public string NombreUsuario
        {
            get {                
                string userName = $"{Nombre.ToLower()}{Apellido}";
                string textoNormalizado = userName.Normalize(NormalizationForm.FormD);
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                string textoSinAcentos = reg.Replace(textoNormalizado, ""); 
                return textoSinAcentos + "_" + Id;                 
            }
        }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Contrasena { get; set; }
        [Required]
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
    }
}
