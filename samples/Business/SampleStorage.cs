using System;
using System.Collections.Generic;
using Contracts.Model;

namespace Business
{
    public class SampleStorage
    {
        public IList<Customer> Customers = new List<Customer>
        {
            new Customer {Id = Guid.NewGuid(), Name = "Alice", BirthDate = new DateTime(1980, 11, 23)},
            new Customer {Id = Guid.NewGuid(), Name = "Bob", BirthDate = new DateTime(1955, 3, 31)},
            new Customer {Id = Guid.NewGuid(), Name = "Charlie", BirthDate = new DateTime(1992, 12, 2)}
        };

        public IList<Order> Orders = new List<Order>
        {
            new Order {Id = Guid.NewGuid(), ShippingAddress = new Address { City = "London", Street = "Downing Street"} }
        };
    }
}