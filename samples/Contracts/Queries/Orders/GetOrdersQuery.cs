using System.Runtime.Serialization;
using Contracts.Model;

namespace Contracts.Queries.Orders
{
    [DataContract]
    public class GetOrdersQuery : PagedQuery<Order>
    {
    }
}