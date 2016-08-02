using System.ComponentModel;
using System.Runtime.Serialization;
using Contracts.Model;

namespace Contracts.Queries.Orders
{
    [DataContract]
    [Description("Query orders")]
    public class GetOrdersQuery : PagedQuery<Order>
    {
    }
}