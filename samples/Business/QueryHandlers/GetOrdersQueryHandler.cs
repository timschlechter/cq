using System.Linq;
using Contracts.Model;
using Contracts.Queries.Orders;
using CQ;

namespace Business.QueryHandlers
{
    public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, Page<Order>>
    {
        private readonly SampleStorage _storage;

        public GetOrdersQueryHandler(SampleStorage storage)
        {
            _storage = storage;
        }

        public Page<Order> Handle(GetOrdersQuery query)
        {
            return new Page<Order>
            {
                Items = _storage.Orders.ApplyPaging(query).ToArray(),
                TotalCount = _storage.Orders.Count
            };
        }
    }
}