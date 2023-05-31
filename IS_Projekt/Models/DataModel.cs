using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Projekt.Models
{

    public class DataModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }
        [ForeignKey("YearId")]
        public YearModel Year { get; set; }
        [Required]
        public string IndividualCriteria { get; set; }
        [Required]
        public string UnitOfMeasure { get; set; }
        [Required]
        public double Value { get; set; }
    }

    public class ECommerce : DataModel { }
    public class InternetUse : DataModel { }
}
