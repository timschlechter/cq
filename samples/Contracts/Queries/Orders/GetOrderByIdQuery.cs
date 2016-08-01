using System;
using System.Runtime.Serialization;
using CQ;
using Contracts.Model;

namespace Contracts.Queries.Orders
{
    [DataContract]
    public class GetOrderByIdQuery : IQuery<Order>
    {
        [DataMember(Name = "orderId")]
        public Guid OrderId { get; set; }
    }
}