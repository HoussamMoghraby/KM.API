using System.ComponentModel.DataAnnotations;

namespace MyApp.Data.Payloads
{
    public class AddUserPayload
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
