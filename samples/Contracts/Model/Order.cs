using System;
using System.Runtime.Serialization;

namespace Contracts.Model
{
    [DataContract]
    public class Order
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "shippingAddress")]
        public Address ShippingAddress { get; set; }
    }
}