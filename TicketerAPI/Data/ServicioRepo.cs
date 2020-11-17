using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class ServicioRepo : IServicioRepo
    {
        private readonly TicketerContext _context;

        public ServicioRepo(TicketerContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Servicio>> GetAll()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio> GetById(int id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateEntity(Servicio entity)
        {
            if(entity == null){
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Servicios.AddAsync(entity);
        }


        public void UpdateEntity(Servicio entity)
        {
            // Nothing
        }
        
        public void DeleteEntity(Servicio entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Servicios.Remove(entity);
        }


        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}