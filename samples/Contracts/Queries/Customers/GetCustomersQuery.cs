using System.ComponentModel;
using System.Runtime.Serialization;
using Contracts.Model;

namespace Contracts.Queries.Customers
{
    [DataContract]
    [Description("Query customers")]
    public class GetCustomersQuery : PagedQuery<Customer>
    {
    }
}