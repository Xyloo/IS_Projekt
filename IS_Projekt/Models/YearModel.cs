using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IS_Projekt.Models
{
    [DataContract]
    public class YearModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataMember]
        public int Year { get; set; }
    }
}
