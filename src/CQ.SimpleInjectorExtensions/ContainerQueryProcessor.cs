using System.Diagnostics;
using SimpleInjector;

namespace CQ
{
    public class ContainerQueryProcessor : IQueryProcessor
    {
        private readonly Container _container;

        public ContainerQueryProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TResult Process<TResult>(IQuery<TResult> query)
        {
            return (dynamic) _container.DelegateQueryToHandler(query);
        }
    }
}