using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class ClienteRepo : IClienteRepo
    {
        private readonly TicketerContext _context;

        public ClienteRepo(TicketerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task CreateEntity(Cliente cliente)
        {
            if(cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            await _context.Clientes.AddAsync(cliente);
        }

        public void UpdateEntity(Cliente cliente)
        {
            // Nothing            
        }

        public void DeleteEntity(Cliente cliente)
        {
            if(cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            _context.Clientes.Remove(cliente);
        }
    }
}