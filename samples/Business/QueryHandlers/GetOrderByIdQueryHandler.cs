using CQ;
using Contracts.Model;
using Contracts.Queries.Orders;

namespace Business.QueryHandlers
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