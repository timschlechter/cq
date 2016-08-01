using System;
using System.Runtime.Serialization;

namespace Contracts.Model
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "birthDate")]
        public DateTime BirthDate{ get; set; }
    }
}