using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class TicketerContext : DbContext
    {
        public TicketerContext(DbContextOptions<TicketerContext> opt)
            : base(opt)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketPrioridad> TicketPrioridades { get; set; }
        public DbSet<TicketStatus> TicketEstados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}