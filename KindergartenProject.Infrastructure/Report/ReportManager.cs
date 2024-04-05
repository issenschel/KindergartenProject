using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KindergartenProject.Infrastructure.Report
{
    public class ReportManager
    {
        public byte[] GenerateReport<TEntity>(IEnumerable<TEntity> info)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Отчёт");
            sheet.Cells["A1"].LoadFromCollection(info, true);
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            var font = sheet.Cells.Style.Font;
            font.Name = "Times New Roman";
            font.Size = 12;
            var color = ColorTranslator.FromHtml("#FF0000"); 
            sheet.Cells.Style.Font.Color.SetColor(color);
            return package.GetAsByteArray();

        }

    }
}
