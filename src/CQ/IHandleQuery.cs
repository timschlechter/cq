namespace CQ
{
    public interface IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}