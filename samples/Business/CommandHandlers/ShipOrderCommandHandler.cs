using Contracts.Commands.Orders;
using Contracts.Model;
using Contracts.Queries.Orders;
using CQ;

namespace Business.CommandHandlers
{
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrderCommand>
    {
        private readonly IQueryProcessor _queryProcessor;

        public ShipOrderCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public void Handle(ShipOrderCommand command)
        {
            var order = _queryProcessor.Process(new GetOrderByIdQuery {OrderId = command.OrderId});

            if (order == null)
            {
                throw new BusinessException($"Order with id {command.OrderId} not found");
            }

            order.Status = OrderStatus.Shipped;
        }
    }
}