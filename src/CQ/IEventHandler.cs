namespace CQ
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent e);
    }
}