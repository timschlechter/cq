using System;
using CQ;
using Samples.Contracts.Commands.Orders;

namespace Samples.Business.CommandHandlers
{
    public class CreateOrderCommandHandler : IHandleCommand<CreateOrderCommand>
    {
        public void Handle(CreateOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}