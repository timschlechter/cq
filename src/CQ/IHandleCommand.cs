namespace CQ
{
    public interface IHandleCommand<TCommand>
    {
        void Handle(TCommand command);
    }
}