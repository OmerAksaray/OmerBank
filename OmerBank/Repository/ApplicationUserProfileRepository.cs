using OmerBank.Models.Context;
using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public class ApplicationUserProfileRepository : Repository<ApplicationUserProfile>, IApplicationUserProfileRepository
    {
        private readonly MyContext _context;
        public ApplicationUserProfileRepository(MyContext context) : base(context)
        {
            _context=context;
        }
    }
}
