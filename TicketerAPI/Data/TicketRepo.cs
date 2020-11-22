using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class TicketRepo : ITicketRepo
    {
        private readonly TicketerContext _context;

        public TicketRepo(TicketerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _context.Tickets
                            .Include(t => t.Prioridad)
                            .Include(t => t.Servicio)
                            .Include(t => t.TicketStatus)
                            .Include(t => t.Usuario).ThenInclude(u => u.Rol)
                            .Include(t => t.Cliente)
                            .ToListAsync();
        }

        public async Task<Ticket> GetById(int id)
        {
            return await _context.Tickets
                            .Include(t => t.Prioridad)
                            .Include(t => t.Servicio)
                            .Include(t => t.TicketStatus)
                            .Include(t => t.Usuario).ThenInclude(u => u.Rol)
                            .Include(t => t.Cliente)
                            .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task CreateEntity(Ticket Ticket)
        {
            if(Ticket == null)
            {
                throw new ArgumentNullException(nameof(Ticket));
            }

            await _context.Tickets.AddAsync(Ticket);
        }

        public void UpdateEntity(Ticket Ticket)
        {
            // Nothing            
        }

        public void DeleteEntity(Ticket Ticket)
        {
            if(Ticket == null)
            {
                throw new ArgumentNullException(nameof(Ticket));
            }
            _context.Tickets.Remove(Ticket);
        }
    }
}