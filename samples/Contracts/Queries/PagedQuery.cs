using System.ComponentModel;
using System.Runtime.Serialization;
using Contracts.Model;
using CQ;

namespace Contracts.Queries
{
    [DataContract]
    public class PagedQuery<T> : IQuery<Page<T>>
    {
        [DataMember(Name = "$skip")]
        [Description("Number of items to skip")]
        public int? Skip { get; set; }

        [DataMember(Name = "$take")]
        [Description("Maximum number of items to return")]
        public int? Take { get; set; }
    }
}