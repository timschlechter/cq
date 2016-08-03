using Contracts.Commands.Orders;
using Contracts.Model;
using Contracts.Queries.Orders;
using CQ;

namespace Business.CommandHandlers
{
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrderCommand>
    {
        private readonly IQueryHandler<GetOrderByIdQuery, Order> _getOrderByIdHandler;

        public ShipOrderCommandHandler(IQueryHandler<GetOrderByIdQuery, Order> getOrderByIdHandler)
        {
            _getOrderByIdHandler = getOrderByIdHandler;
        }

        public void Handle(ShipOrderCommand command)
        {
            var order = _getOrderByIdHandler.Handle(new GetOrderByIdQuery {OrderId = command.OrderId});

            if (order == null)
            {
                throw new BusinessException($"Order with id {command.OrderId} not found");
            }

            order.Status = OrderStatus.Shipped;
        }
    }
}