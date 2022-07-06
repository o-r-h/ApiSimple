using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers.Excel
{
    public class ExcelChartTypeLine
    {
        public string Title { get; set; }
        public string Legend { get; set; }
        public int LegendPosition { get; set; } = ChartLegendPosition.Bottom;
        public int SizeX { get; set; } = 10;
        public int SizeY { get; set; } = 10;
        public ChartPosition Position { get; set; } 
        public ExcelChartRange ExcelChartRange { get; set; }

    }

    public class ExcelChartRange
    {
        public int FromRow { get; set; }
        public int FromCol { get; set; }
        public int ToRow { get; set; }
        public int ToCol { get; set; }
    }

    public class ChartPosition
    {
        public int Row { get; set; }
        public int RowOffSetPixel { get; set; }
        public int Col { get; set; }
        public int ColOffSetPixel { get; set; }


    }

    public sealed class ChartLegendPosition
    {
        public const int Bottom = 3;
        public const int Left = 1;
        public const int Right = 2;
        public const int Top = 0;
        public const int TopRight = 4;

    }


}
