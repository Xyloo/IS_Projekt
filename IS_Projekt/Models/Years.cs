using System.ComponentModel.DataAnnotations;

namespace IS_Projekt.Models
{
    public class Years
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
