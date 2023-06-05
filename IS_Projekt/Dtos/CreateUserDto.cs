using System.ComponentModel.DataAnnotations;

namespace IS_Projekt.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }

}
