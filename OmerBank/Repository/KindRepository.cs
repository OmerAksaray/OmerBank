using Microsoft.EntityFrameworkCore;
using OmerBank.Models.Context;
using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public class KindRepository : Repository<Kind>, IKindRepository
    {
        private readonly MyContext _context;
        private readonly DbSet<Kind> _dbSet;

        private readonly IKindRepository _kindRepository;
        public KindRepository(MyContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Kind>();

        }

        public async Task<Kind> GetByKindWithAccountIdAsync(int id, string name)
        {
            try
            {
                Console.WriteLine($"Fetching Kind with AccountID: {id} and Name: {name}");
                return await _dbSet.FirstOrDefaultAsync(a => a.AccountID == id && a.Name == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByKindWithAccountIdAsync: {ex.Message}");
                throw;
            }
        }

    }
}
