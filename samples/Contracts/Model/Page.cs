using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Contracts.Model
{
    [DataContract]
    public class Page<T>
    {
        [Required]
        [DataMember(Name = "items")]
        public IList<T> Items { get; set; }

        [Required]
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }
}