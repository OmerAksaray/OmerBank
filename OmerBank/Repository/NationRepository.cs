using OmerBank.Models.Context;
using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public class NationRepository : Repository<Nation>, INationRepository
    {
        private readonly MyContext _context;
        public NationRepository(MyContext context) : base(context)
        {
            _context = context;
        }
    }
}
