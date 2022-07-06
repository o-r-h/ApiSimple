using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers.Excel
{
    public class ExcelFile
    {
        public const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public string ExcelFileName { get; set; }
        public string PrefixSheetName { get; set; }
        public List<ExcelWorkSheet> ExcelWorkSheets { get; set; }

    }
}
