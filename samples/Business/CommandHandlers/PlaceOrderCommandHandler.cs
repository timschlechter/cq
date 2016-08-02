using System;
using CQ;
using Contracts.Commands.Orders;
using Contracts.Model;

namespace Business.CommandHandlers
{
    public class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand>
    {
        private readonly SamplesStorage _storage;

        public PlaceOrderCommandHandler(SamplesStorage storage)
        {
            _storage = storage;
        }

        public void Handle(PlaceOrderCommand command)
        {
            _storage.Orders.Add(new Order
            {
                Id = command.OrderId,
                ShippingAddress = command.ShippingAddress,
                Status = OrderStatus.Created
            });
        }
    }
}