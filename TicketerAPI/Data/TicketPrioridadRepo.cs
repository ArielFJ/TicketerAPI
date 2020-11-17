using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class TicketPrioridadRepo : ITicketPrioridadRepo
    {
        private readonly TicketerContext _context;

        public TicketPrioridadRepo(TicketerContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<TicketPrioridad>> GetAll()
        {
            return await _context.TicketPrioridades.ToListAsync();
        }

        public async Task<TicketPrioridad> GetById(int id)
        {
            return await _context.TicketPrioridades.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateEntity(TicketPrioridad entity)
        {
            if(entity == null){
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.TicketPrioridades.AddAsync(entity);
        }


        public void UpdateEntity(TicketPrioridad entity)
        {
            // Nothing
        }
        
        public void DeleteEntity(TicketPrioridad entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.TicketPrioridades.Remove(entity);
        }


        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}