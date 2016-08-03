using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Contracts.Model;
using CQ;

namespace Contracts.Queries.Orders
{
    [DataContract]
    [Description("Returns the order by the given id")]
    public class GetOrderByIdQuery : IQuery<Order>
    {
        [Required]
        [Description("Id of the order to query")]
        [DataMember(Name = "orderId")]
        public Guid OrderId { get; set; }
    }
}