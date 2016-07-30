using System;

namespace Samples.Contracts.Commands.Orders
{
    public class ShipOrderCommand
    {
        public Guid OrderId { get; set; }
    }
}