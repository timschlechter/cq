using System.Linq;
using Contracts.Model;
using Contracts.Queries.Orders;
using CQ;

namespace Business.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, Order>
    {
        private readonly SampleStorage _storage;

        public GetOrderByIdQueryHandler(SampleStorage storage)
        {
            _storage = storage;
        }

        public Order Handle(GetOrderByIdQuery query)
        {
            return _storage.Orders.FirstOrDefault(order => order.Id == query.OrderId);
        }
    }
}