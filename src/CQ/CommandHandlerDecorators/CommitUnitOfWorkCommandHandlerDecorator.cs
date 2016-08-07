namespace CQ.CommandHandlerDecorators
{
    public class CommitUnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly IUnitOfWork _unitOfWork;

        public CommitUnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> decorated, IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        public void Handle(TCommand command)
        {
            _decorated.Handle(command);
            _unitOfWork.Commit();
        }
    }
}