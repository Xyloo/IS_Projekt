using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IS_Projekt.Models
{
    [DataContract]
    public class CountryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataMember]
        public string CountryName { get; set; }
        [Required]
        [DataMember]
        public string CountryCode { get; set; }
    }
}
