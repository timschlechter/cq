using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CQ.CommandHandlerDecorators
{
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;

        public ValidationCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            _decorated = decorated;
        }

        [DebuggerStepThrough]
        public void Handle(TCommand command)
        {
            var validationContext = new ValidationContext(command, null, null);
            Validator.ValidateObject(command, validationContext, true);

            _decorated.Handle(command);
        }
    }
}