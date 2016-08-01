using System;
using CQ;
using Contracts.Commands.Orders;

namespace Business.CommandHandlers
{
    public class ShipOrderCommandHandler : IHandleCommand<ShipOrderCommand>
    {
        public void Handle(ShipOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}