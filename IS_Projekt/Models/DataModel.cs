using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace IS_Projekt.Models
{

    [DataContract]
    [KnownType(typeof(ECommerce))]
    [KnownType(typeof(InternetUse))]
    public class DataModel
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [ForeignKey("CountryId")]
        [DataMember]
        public CountryModel Country { get; set; }
        [ForeignKey("YearId")]
        [DataMember]
        public YearModel Year { get; set; }
        [Required]
        [DataMember]
        public string IndividualCriteria { get; set; }
        [Required]
        [DataMember]
        public string UnitOfMeasure { get; set; }
        [Required]
        [DataMember]
        public double Value { get; set; }
    }

    [DataContract]
    public class ECommerce : DataModel { }
    [DataContract]
    public class InternetUse : DataModel { }
}
