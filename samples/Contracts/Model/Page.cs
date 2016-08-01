using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Model
{
    public class Page<T>
    {
        [DataMember(Name="items")]
        public IList<T> Items { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
