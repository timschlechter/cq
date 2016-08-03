using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Contracts.Model;

namespace Contracts.Commands.Orders
{
    [DataContract]
    [Description("Places a new order")]
    public class PlaceOrderCommand
    {
        [Required]
        [DataMember(Name = "orderId")]
        public Guid OrderId { get; set; }

        [Required]
        [DataMember(Name = "shippingAddress")]
        public Address ShippingAddress { get; set; }
    }
}