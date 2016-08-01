using System.Runtime.Serialization;

namespace Contracts.Model
{
    [DataContract]
    public class Address
    {
        [DataMember(Name="street")]
        public string Street { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }
    }
}