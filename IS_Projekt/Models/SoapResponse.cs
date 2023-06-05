using System.Runtime.Serialization;

namespace IS_Projekt.Models
{
    [DataContract]
    public class SoapResponse<T>
    {
        [DataMember]
        public string? Message { get; set; }
        [DataMember]
        public string? Error { get; set; }
        [DataMember]
        public T? Data { get; set; }
    }
}
