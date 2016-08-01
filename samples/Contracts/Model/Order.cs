using System;

namespace Contracts.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public Address ShippingAddress { get; set; }
    }
}