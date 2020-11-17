using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class RolRepo : IRolRepo
    {
        private readonly TicketerContext _context;

        public RolRepo(TicketerContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Rol>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> GetById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateEntity(Rol entity)
        {
            if(entity == null){
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Roles.AddAsync(entity);
        }


        public void UpdateEntity(Rol entity)
        {
            // Nothing
        }
        
        public void DeleteEntity(Rol entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Roles.Remove(entity);
        }


        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}