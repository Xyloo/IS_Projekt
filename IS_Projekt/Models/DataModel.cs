using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Projekt.Models
{
    public interface IDataModel
    {
        int Id { get; set; }
        int CountryId { get; set; }
        int YearId { get; set; }

        Countries Country { get; set; }
        Years Year { get; set; }
        string DataType { get; set; }
        string IndividualCriteria { get; set; }
        string UnitOfMeasure { get; set; }
        double Value { get; set; }
    }


    public class DataModel : IDataModel
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int YearId { get; set; }

        [ForeignKey("CountryId")]
        public Countries Country { get; set; }
        [ForeignKey("YearId")]
        public Years Year { get; set; }
        [Required]
        public string DataType { get; set; }
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
