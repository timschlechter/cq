using System;

namespace Samples.Contracts.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public Address ShippingAddress { get; set; }
    }
}