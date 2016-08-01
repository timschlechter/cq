using System;
using CQ;
using Contracts.Model;

namespace Contracts.Queries.Orders
{
    public class GetOrderByIdQuery : IQuery<Order>
    {
        public Guid OrderId { get; set; }
    }
}