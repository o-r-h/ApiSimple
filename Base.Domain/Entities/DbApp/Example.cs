using Sieve.Attributes;
using System;
using System.Collections.Generic;

#nullable disable

namespace Base.Domain.Entities.DbApp
{
    public partial class Example 
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public long IdExample { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
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
