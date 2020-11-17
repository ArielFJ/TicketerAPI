using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class TicketStatusRepo : ITicketStatusRepo
    {
        private readonly TicketerContext _context;

        public TicketStatusRepo(TicketerContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<TicketStatus>> GetAll()
        {
            return await _context.TicketEstados.ToListAsync();
        }

        public async Task<TicketStatus> GetById(int id)
        {
            return await _context.TicketEstados.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateEntity(TicketStatus entity)
        {
            if(entity == null){
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.TicketEstados.AddAsync(entity);
        }


        public void UpdateEntity(TicketStatus entity)
        {
            // Nothing
        }
        
        public void DeleteEntity(TicketStatus entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.TicketEstados.Remove(entity);
        }


        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}