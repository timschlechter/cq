using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using CQ;
using Contracts.Model;
using System.ComponentModel.DataAnnotations;

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