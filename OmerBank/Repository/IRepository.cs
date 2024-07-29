using OmerBank.Models.Entities;
using System.Linq;

namespace OmerBank.Repository
{
    public interface IRepository<T> where T : BaseEntitiy
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);

        void DeleteById(int id);
  
    }

}
