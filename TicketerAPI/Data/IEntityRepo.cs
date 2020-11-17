using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketerAPI.Data
{
    public interface IEntityRepo<T>
    {
        Task<bool> SaveChanges();


        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task CreateEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }
}