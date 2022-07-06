using Base.Domain.Helpers.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers.Excel
{
    public class ExcelWorkSheet
    {
        public string Name { get; set; }
        public List<ExcelCellStyle> ExcelCellStyles { get; set; }
        public List<Cell> Cells { get; set; }
        public List<ExcelChartTypeLine> ChartLines { get; set; }

    }
}
