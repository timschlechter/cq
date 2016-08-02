using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Model;
using Contracts.Queries.Customers;
using CQ;

namespace Business.QueryHandlers
{
    public class GetCustomersQueryHandler : IHandleQuery<GetCustomersQuery, Page<Customer>>
    {
        private static readonly List<Customer> _customers = new List<Customer>
        {
            new Customer {Id = Guid.NewGuid(), Name = "Alice", BirthDate = new DateTime(1980, 11, 23)},
            new Customer {Id = Guid.NewGuid(), Name = "Bob", BirthDate = new DateTime(1955, 3, 31)},
            new Customer {Id = Guid.NewGuid(), Name = "Charlie", BirthDate = new DateTime(1992, 12, 2)}
        };

        public Page<Customer> Handle(GetCustomersQuery query)
        {
            return new Page<Customer>()
            {
                Items = _customers.ApplyPaging(query).ToArray(),
                TotalCount = _customers.Count
            };
        }
    }
}