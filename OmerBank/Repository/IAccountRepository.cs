using OmerBank.Models.Entities;

namespace OmerBank.Repository
{
    public interface IAccountRepository:IRepository<Account>
    {
        Task<Account> GetByIbanAsync(int iban);
        Task DepositAsync(int iban, string name, decimal amount);

        Task WithDraw(int iban, string name, decimal amount);

        Task TransferMoney(int iban, int transferIban, string name, decimal amount);

        Task ConvertMoney(int id, string name, string convertName, decimal amount);
    }
}
