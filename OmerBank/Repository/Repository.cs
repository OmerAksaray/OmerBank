
using Microsoft.EntityFrameworkCore;
using OmerBank.Models.Context;
using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntitiy
    {
        private readonly MyContext _context;

        private readonly DbSet<T> _dbSet;

        public Repository(MyContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public  void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return  await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
             _dbSet.Update(entity);

            await _context.SaveChangesAsync();
            
        }
        public void DeleteById(int id)
        {
            var entity = _dbSet.FirstOrDefault(a => a.ID == id);
            Delete(entity);
        }

      
    }
}
