using CQ;
using Samples.Contracts.Model;
using Samples.Contracts.Queries.Orders;

namespace Samples.Business.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IHandleQuery<GetOrderByIdQuery, Order>
    {
        public Order Handle(GetOrderByIdQuery query)
        {
            return new Order
            {
                Id = query.OrderId
            };
        }
    }
}