using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Contracts.Model
{
    [DataContract]
    public class Customer
    {
        [Required]
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "birthDate")]
        public DateTime? BirthDate{ get; set; }
    }
}