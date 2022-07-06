using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers.Excel
{
    public class ExcelCellStyle
    {
        public string ExcelStyleName { get; set; }
        public int FontSize { get; set; } = 8;
        public bool FontBold { get; set; } = false;
        public string FontName { get; set; } = "Verdana";
        public RGBCustom FontRGB { get; set; }
        
        public int TopBorderStyle { get; set; } = BorderStyle.Thin;
        public int BottomBorderStyle { get; set; } = BorderStyle.Thin;
        public int LeftBorderStyle { get; set; } = BorderStyle.Thin;
        public int RightBorderStyle { get; set; } = BorderStyle.Thin;
        public int PatternType { get; set; } = 1; //Solid
        public RGBCustom BackGroundRGB { get; set; }
        


        public int HorizontalAlignment { get; set; }
        public int VerticalAlignment { get; set; }

    }

    public sealed class BorderStyle
    {
        public const int DashDot = 3;
        public const int DashDotDot = 5;
        public const int Dashed = 6;
        public const int Dotted = 2;
        public const int Double = 12;
        public const int Hair = 1;
        public const int Medium = 11;
        public const int MediumDashDot = 9;
        public const int MediumDashDotDot = 7;
        public const int MediumDashed = 8;
        public const int None = 0;
        public const int Thick = 10;
        public const int Thin = 4;

    }


    public sealed class ExcelVerticalAlignment
    {
        public const int Bottom = 2;
        public const int Center = 1;
        public const int Distributed = 3;
        public const int Justify = 4;
        public const int Top = 0;

    }

    public sealed class HorizontalAlignment
    {
        public const int Center = 2;
        public const int CenterContinuous = 3;
        public const int Distributed = 6;
        public const int Fill = 5;
        public const int General = 0;
        public const int Justify = 7;
        public const int Left = 1;
        public const int Right = 4;

    }

    public class RGBCustom
    {
        public int RColor { get; set; }
        public int GColor { get; set; }
        public int BColor { get; set; }

        public RGBCustom GenerateRgb(string backgroundColor)
        {
            RGBCustom rGBCustom = new RGBCustom();
            Color color = ColorTranslator.FromHtml(backgroundColor);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);
            rGBCustom.RColor = r;
            rGBCustom.GColor = g;
            rGBCustom.BColor = b;
            return rGBCustom;
        }
    }





}
