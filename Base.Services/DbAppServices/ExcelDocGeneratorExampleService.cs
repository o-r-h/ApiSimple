using Base.Domain.Entities.DbApp;
using Base.Domain.Helpers;
using Base.Domain.Helpers.Excel;
using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Domain.Interfaces.DbApp.ServiceInterface;
using OfficeOpenXml;
using OfficeOpenXml.Style.XmlAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Services.DbAppServices
{
    public class ExcelDocGeneratorExampleService : IExcelDocGeneratorExampleService
    {
        private readonly IExampleRepository exampleRepository;
        public ExcelDocGeneratorExampleService(IExampleRepository exampleRepository)
        {
            this.exampleRepository = exampleRepository;
        }

    
        private List<ExcelCellStyle> SetExcelCellStyles()
        {
            List<ExcelCellStyle> excelCellStyles = new List<ExcelCellStyle>();
            RGBCustom rGBCustom = new RGBCustom()
            {
                RColor = 190,
                BColor = 0,
                GColor = 0
            };

            ExcelCellStyle styleHeader = new ExcelCellStyle();
            styleHeader.ExcelStyleName = "HeaderCustom";
            styleHeader.FontSize = 16;
            styleHeader.FontName = "Arial";
            styleHeader.FontBold = true;
            styleHeader.BackGroundRGB = rGBCustom;
            styleHeader.FontRGB = rGBCustom;

            excelCellStyles.Add(styleHeader);
            return excelCellStyles;
        }


        public async Task<byte[]> ExampleCreateExcelEPPLUS()
        {
          
            ExcelPackage expack = new ExcelPackage();
            ExcelFile xls = new ExcelFile();
            ExcelWorkSheet sheet = new ExcelWorkSheet();
            List<ExcelCellStyle> excelCellStyles = new List<ExcelCellStyle>();

            sheet.Cells = new List<Cell>();
            xls.ExcelWorkSheets = new List<ExcelWorkSheet>();
            xls.ExcelFileName = "TestExcel";

            sheet.Name = "Sheet nr 1";

            excelCellStyles = SetExcelCellStyles();

            xls.ExcelWorkSheets.Add(sheet);

            xls.ExcelWorkSheets[0].ExcelCellStyles = excelCellStyles;

            List<Cell> cellList = new List<Cell>();
            Cell cell = new Cell { ColPos = 1, RowPos = 1, Value = "TEST", Style = sheet.ExcelCellStyles[0] };
            cellList.Add(cell);
            sheet.Cells.Add(cell);

            cellList = ExcelHelper.CreateCellTable<Example>(2, 2, (List<Example>)await GetAllexamples());

            foreach (var item in cellList)
            {
                sheet.Cells.Add(item);
            }


            foreach (var item in xls.ExcelWorkSheets)
            {
                expack.Workbook.Worksheets.Add(item.Name);
            }



            int x = 0;
            foreach (var item in xls.ExcelWorkSheets)
            {
                foreach (var subitem in item.Cells)
                {
                    expack.Workbook.Worksheets[x].Cells[subitem.RowPos, subitem.ColPos].Value = subitem.Value;
                }
                x++;
            }

            //TODO apply style for al cells  using cell.style property
            //expack.Workbook.Worksheets[1].Cells[1, 1].Style.Font.Bold = styleHeader.FontBold;
            //expack.Workbook.Worksheets[1].Cells[1, 1].Style.Font.Size = styleHeader.FontSize;

            foreach (var item in xls.ExcelWorkSheets)
            {
                foreach (var subitem in item.ExcelCellStyles)
                {
                    ExcelNamedStyleXml estilo = expack.Workbook.Styles.CreateNamedStyle(subitem.ExcelStyleName);
                    estilo.Style.Font.Size = subitem.FontSize;
                    estilo.Style.Font.Bold = subitem.FontBold;
                    estilo.Style.Font.Name = subitem.FontName;
                    estilo.Style.Font.Color.SetColor(0, subitem.FontRGB.RColor, subitem.FontRGB.GColor, subitem.FontRGB.BColor);
                    estilo.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    estilo.Style.Fill.BackgroundColor.SetColor(0, subitem.BackGroundRGB.RColor, subitem.BackGroundRGB.GColor, subitem.BackGroundRGB.BColor);
                    estilo.Style.Border.Bottom.Style = (OfficeOpenXml.Style.ExcelBorderStyle)subitem.BottomBorderStyle;
                    estilo.Style.Border.Top.Style = (OfficeOpenXml.Style.ExcelBorderStyle)subitem.TopBorderStyle;
                    estilo.Style.Border.Right.Style = (OfficeOpenXml.Style.ExcelBorderStyle)subitem.RightBorderStyle;
                    estilo.Style.Border.Left.Style = (OfficeOpenXml.Style.ExcelBorderStyle)subitem.LeftBorderStyle;
                    estilo.Style.VerticalAlignment = (OfficeOpenXml.Style.ExcelVerticalAlignment)subitem.VerticalAlignment;
                }
            }

            return expack.GetAsByteArray(); 

        }

     

        private async Task<IEnumerable<Example>> GetAllexamples()
        {
           
            return await exampleRepository.GetAll();
        }





    }
}
