namespace CQ
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}