using ClosedXML.Excel;
using TechInvent.BLL.Extensions;
using TechInvent.BLL.Service;
using TechInvent.DM.Models;
namespace WebMVC.Services
{
    public partial class ExcelService
    {
        private readonly EntityCheckerService _entityChecker;

        public ExcelService(EntityCheckerService entityChecker)
        {
            _entityChecker = entityChecker;
        }

        public async Task<MemoryStream> FileEquipmentExampleAsync()
        {
            var memoryStream = new MemoryStream();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Оборудование");
                worksheet.Cell("A1").Value = "Наименование объекта";
                worksheet.Cell("B1").Value = "Инвентарный";
                worksheet.Cell("D1").Value = "Серийный номер";
                worksheet.Cell("E1").Value = "Производитель";
                worksheet.Cell("F1").Value = "Место нахождения";
                worksheet.Cell("F1").Value = "Тип оборудования";
                worksheet.Columns().AdjustToContents();
                worksheet.Column(3).Width = 2;
                worksheet.Range("B1:C1").Merge();
                worksheet.Range("A1:F1").Style.Font.Bold = true;
                worksheet.Range("A1:F1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range("A1:F1").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Range("A1:F100").Style
                        .Border.SetTopBorder(XLBorderStyleValues.Thin)
                        .Border.SetRightBorder(XLBorderStyleValues.Thin)
                        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
                        
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<List<CabinetEquipment>> ImportEquipmentAsync(MemoryStream stream)
        {
            List<CabinetEquipment> equipments = new List<CabinetEquipment>();
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                var rowCount = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= rowCount; row++)
                {
                    var equipment = new CabinetEquipment
                    {
                        Name = worksheet.Cell(row, 1).GetString(),
                        InventNumber = $"{worksheet.Cell(row, 2).GetString()} {worksheet.Cell(row, 3).GetString()}".ReturnNullIfEmpty(),
                        SerialNumber = worksheet.Cell(row, 4).GetString().ReturnNullIfEmpty(),
                        Vendor = !worksheet.Cell(row, 5).GetString().IsEmpty() ? await _entityChecker.GetOrCreateVendorAsync(worksheet.Cell(row, 5).GetString()) : null,
                        Cabinet = !worksheet.Cell(row, 6).GetString().IsEmpty() ? await _entityChecker.GetOrCreateCabinetAsync(worksheet.Cell(row, 6).GetString()) : null,
                        CabinetEquipmentType = await _entityChecker.GetOrCreateCabinetEquipmentTypeAsync(worksheet.Cell(row, 7).GetString())
                    };
                    equipments.Add(equipment);
                }
                return equipments;
            }
        }
    }
}
