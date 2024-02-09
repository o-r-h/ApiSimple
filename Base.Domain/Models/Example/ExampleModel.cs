using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Models.Example
{
    public class ExampleModel
    {
        [Sieve(CanFilter = true, CanSort = true)]
        //public long IdExample { get; set; }
        //[Sieve(CanFilter = true, CanSort = true)]
        public string NameExample { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal? PriceExample { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime? CreatedAt { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedBy { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime? ModifiedAt { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string ModifiedBy { get; set; }
    }
}
