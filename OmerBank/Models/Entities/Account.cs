namespace OmerBank.Models.Entities
{
    public class Account:BaseEntitiy
    {
        public int IBAN { get; set; }

        public int AppUserProfileID{ get; set; }

        public int? NationID { get; set; }

        //relation

        public ApplicationUserProfile AppUserProfile{ get; set; }

        public ICollection<Kind> Kinds { get; set; }

        public Nation Nation{ get; set; }

        public Account(int userProfileId)
        {
            IBAN = userProfileId;
            AppUserProfileID = userProfileId;
        }
        public Account()
        {
            
        }
    }
}
