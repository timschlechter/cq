using System;
using CQ;
using Samples.Contracts.Model;

namespace Samples.Contracts.Queries.Orders
{
    public class GetOrderByIdQuery : IQuery<Order>
    {
        public Guid OrderId { get; set; }
    }
}