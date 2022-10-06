using System.ComponentModel.DataAnnotations;

namespace MyApp.Data.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
