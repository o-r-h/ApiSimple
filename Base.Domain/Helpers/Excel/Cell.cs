using Base.Domain.Helpers.Excel;

namespace Base.Domain.Helpers.Excel
{
    public class Cell
    {
        public int ColPos { get; set; }
        public int RowPos { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }

        public ExcelCellStyle Style { get; set; }
        

    }
}
