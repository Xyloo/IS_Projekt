using System.ComponentModel.DataAnnotations;

namespace IS_Projekt.Models
{
    public class DataModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DataType { get; set; }
        [Required]
        public string IndividualCriteria { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string UnitOfMeasure { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double Value { get; set; }
    }
}
