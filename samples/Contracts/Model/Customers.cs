using System;

namespace Contracts.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate{ get; set; }
    }
}