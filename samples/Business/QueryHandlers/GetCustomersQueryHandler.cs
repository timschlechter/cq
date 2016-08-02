﻿using System.Linq;
using Contracts.Model;
using Contracts.Queries.Customers;
using CQ;

namespace Business.QueryHandlers
{
    public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, Page<Customer>>
    {
        private readonly SamplesStorage _storage;

        public GetCustomersQueryHandler(SamplesStorage storage)
        {
            _storage = storage;
        }

        public Page<Customer> Handle(GetCustomersQuery query)
        {
            return new Page<Customer>
            {
                Items = _storage.Customers.ApplyPaging(query).ToArray(),
                TotalCount = _storage.Customers.Count
            };
        }
    }
}