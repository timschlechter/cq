using System.Linq;
using Contracts.Model;
using Contracts.Queries.Orders;
using CQ;

namespace Business.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, Order>
    {
        private readonly SamplesStorage _storage;

        public GetOrderByIdQueryHandler(SamplesStorage storage)
        {
            _storage = storage;
        }

        public Order Handle(GetOrderByIdQuery query)
        {
            return _storage.Orders.FirstOrDefault(order => order.Id == query.OrderId);
        }
    }
}