using System;
using CQ;
using Contracts.Commands.Orders;

namespace Business.CommandHandlers
{
    public class CreateOrderCommandHandler : IHandleCommand<CreateOrderCommand>
    {
        public void Handle(CreateOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}