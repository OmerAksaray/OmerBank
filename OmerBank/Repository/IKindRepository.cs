using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public interface IKindRepository:IRepository<Kind>
    {
        Task<Kind> GetByKindWithAccountIdAsync(int id, string name);
    }
}
