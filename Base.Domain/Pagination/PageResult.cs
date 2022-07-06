using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Pagination
{
    public class PageResult
    {
        public Object ResultList { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalPageNo { get; set; }
        public int RowsPerPage { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
}
