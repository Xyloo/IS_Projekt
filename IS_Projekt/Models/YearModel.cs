using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Projekt.Models
{
    public class YearModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
