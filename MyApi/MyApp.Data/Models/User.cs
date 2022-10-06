using System.ComponentModel.DataAnnotations;

namespace MyApp.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Province { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Country Country { get; set; }

        [Required]
        public Role Role { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
