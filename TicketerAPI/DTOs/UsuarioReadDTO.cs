namespace TicketerAPI.DTOs
{
    public class UsuarioReadDTO
    {                
        public int Id { get; set; }        
        public string Nombre { get; set; }        
        public string Apellido { get; set; }
        public string NombreUsuario { get => $"{Nombre.ToLower()}{Apellido}_{Id}"; }
        
        public int RolId { get; set; }
    }
}