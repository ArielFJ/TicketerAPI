using System.Text;
using System.Text.RegularExpressions;
using TicketerAPI.Models;

namespace TicketerAPI.DTOs
{
    public class UsuarioReadDTO
    {                
        public int Id { get; set; }        
        public string Nombre { get; set; }        
        public string Apellido { get; set; }
        public string NombreUsuario {
            get {                
                string userName = $"{Nombre.ToLower()}{Apellido}";
                string textoNormalizado = userName.Normalize(NormalizationForm.FormD);
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                string textoSinAcentos = reg.Replace(textoNormalizado, ""); 
                return textoSinAcentos + "_" + Id;                 
            }
        }      
        public string Contrasena { get; set; }
        public Rol Rol { get; set; }
    }
}