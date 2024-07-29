using System.ComponentModel.DataAnnotations;

namespace OmerBank.Models.Entities
{
    public class ApplicationUserProfile:BaseEntitiy

    {
        [Required]
        public string UserName { get; set; }

        public string? Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }




  

        public ApplicationUserProfile(string userName, string? email, string password, string? phoneNumber)
        {
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }
        public ApplicationUserProfile()
        {
            
        }
    }
}
