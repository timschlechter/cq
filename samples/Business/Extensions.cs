using System.Collections.Generic;
using System.Linq;
using Contracts.Queries;

namespace Business
{
    public static class Extensions
    {
        public static IEnumerable<TResult> ApplyPaging<TQuery, TResult>(this IEnumerable<TResult> items, TQuery query)
            where TQuery : PagedQuery<TResult>
        {
            if (query.Skip.HasValue)
            {
                items = items.Skip(query.Skip.Value);
            }

            if (query.Take.HasValue)
            {
                items = items.Take(query.Take.Value);
            }

            return items;
        }
    }
}