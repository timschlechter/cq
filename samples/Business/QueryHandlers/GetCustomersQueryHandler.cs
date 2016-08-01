using Contracts.Model;
using Contracts.Queries.Customers;
using CQ;

namespace Business.QueryHandlers
{
    public class GetCustomersQueryHandler : IHandleQuery<GetCustomersQuery, Page<Customer>>
    {
        public Page<Customer> Handle(GetCustomersQuery query)
        {
            return null;
        }
    }
}