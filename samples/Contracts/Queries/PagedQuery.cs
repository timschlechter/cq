using System.Runtime.Serialization;
using Contracts.Model;
using CQ;

namespace Contracts.Queries
{
    public class PagedQuery<T> : IQuery<Page<T>>
    {
        [DataMember(Name = "$skip")]
        public int? Skip { get; set; }

        [DataMember(Name = "$take")]
        public int? Take { get; set; }
    }
}