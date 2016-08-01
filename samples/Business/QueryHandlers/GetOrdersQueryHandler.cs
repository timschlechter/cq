using CQ;
using Contracts.Model;
using Contracts.Queries.Orders;

namespace Business.QueryHandlers
{
    public class GetOrdersQueryHandler : IHandleQuery<GetOrdersQuery, Page<Order>>
    {
        public Page<Order> Handle(GetOrdersQuery query)
        {
            return null;
        }
    }
}