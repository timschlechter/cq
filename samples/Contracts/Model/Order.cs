using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Contracts.Model
{
    [DataContract]
    public class Order
    {
        [Required]
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [Required]
        [DataMember(Name = "shippingAddress")]
        public Address ShippingAddress { get; set; }

        [Required]
        [DataMember(Name = "status")]
        public OrderStatus Status { get; set; }
    }
}