using System.Runtime.Serialization;
using Contracts.Model;

namespace Contracts.Queries.Customers
{
    [DataContract]
    public class GetCustomersQuery : PagedQuery<Customer>
    {
    }
}