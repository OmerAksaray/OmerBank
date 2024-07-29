namespace OmerBank.Models.Entities
{
    public abstract class BaseEntitiy
    {
        public int ID { get; set; }

        public DateTime? CreateTime{ get; set; }

        public DateTime? UpdateTime{ get; set; }

        public BaseEntitiy()
        {
            CreateTime = DateTime.Now;
        }
    }
}
