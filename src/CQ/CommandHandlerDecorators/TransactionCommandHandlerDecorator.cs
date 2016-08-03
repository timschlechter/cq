using System.Diagnostics;
using System.Transactions;

namespace CQ.CommandHandlerDecorators
{
    public class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;

        public TransactionCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            _decorated = decorated;
        }

        [DebuggerStepThrough]
        public void Handle(TCommand command)
        {
            using (var txs = new TransactionScope())
            {
                _decorated.Handle(command);

                txs.Complete();
            }
        }
    }
}