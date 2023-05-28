using System.ComponentModel.DataAnnotations;

namespace IS_Projekt.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
