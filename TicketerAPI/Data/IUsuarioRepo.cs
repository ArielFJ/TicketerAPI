using System.Collections.Generic;
using TicketerAPI.Models;

namespace TicketerAPI.Data
{
    public interface IUsuarioRepo : IEntityRepo<Usuario>
    {
        bool CheckUser(string username, string password, out Usuario usuario);
    }
}