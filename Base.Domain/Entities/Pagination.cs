using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities
{
    public class Pagination<T>
    {
        public int TotalRows { get; set; }
        public List<T> List { get; set; }
    }
}
