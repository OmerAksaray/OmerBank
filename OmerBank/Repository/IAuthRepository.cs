using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public interface IAuthRepository
    {

        Task<ApplicationUserProfile> Register(ApplicationUserProfile userRepository, string password);

        Task<ApplicationUserProfile> Login(string username, string password);

        Task<bool> UserExist(string userName);
    }
}
