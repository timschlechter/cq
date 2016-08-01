using System;
using System.Runtime.Serialization;

namespace Contracts.Commands.Orders
{
    [DataContract]
    public class ShipOrderCommand
    {
        [DataMember(Name = "orderId")]
        public Guid OrderId { get; set; }
    }
}