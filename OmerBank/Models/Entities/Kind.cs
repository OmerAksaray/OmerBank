
namespace OmerBank.Models.Entities
{
    public class Kind : BaseEntitiy
    {
        public string Name { get; set; }
        public decimal? Money { get; set; }
        public int? AccountID { get; set; } // Account ile ilişki için

        // Relation
        public Account Account { get; set; } // Many-to-One
        public Kind()
        {
            
        }

        public Kind(string name, decimal money, int accountId)
        {
            Name = name;
            Money = money;
            AccountID = accountId;
            
        }
    }
}
