using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly TicketerContext _context;

        public UsuarioRepo(TicketerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task CreateEntity(Usuario usuario)
        {
            if(usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            await _context.Usuarios.AddAsync(usuario);
        }

        public void UpdateEntity(Usuario usuario)
        {
            // Nothing            
        }

        public void DeleteEntity(Usuario usuario)
        {
            if(usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }
            _context.Usuarios.Remove(usuario);
        }

        public bool CheckUser(string username, string password, out Usuario usuario)
        {            
            var user = _context.Usuarios.ToList().Where(u => u.NombreUsuario == username).FirstOrDefault();
            if(user == null)
            {
                usuario = null;
                return false;
            }

            usuario = user;
            return user.Contrasena == password ? true : false;
        }
    }
}