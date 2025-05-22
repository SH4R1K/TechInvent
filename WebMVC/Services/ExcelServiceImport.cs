using ClosedXML.Excel;
using System.IO;
using TechInvent.BLL.Dto;
using TechInvent.BLL.DtoModels;
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

        public async Task<List<CabinetEquipment>>ImportEquipmentAsync(MemoryStream stream)
        {
            List<CabinetEquipment> equipments = new List<CabinetEquipment>();
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1); 
                var rowCount = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= rowCount; row++) 
                {
                    var cabinet = worksheet.Cell(row, 9).GetString().IsEmpty();
                    var equipment = new CabinetEquipment
                    {
                        Name = worksheet.Cell(row, 1).GetString(),
                        InventNumber = $"{worksheet.Cell(row, 3).GetString()} {worksheet.Cell(row, 4).GetString()}".ReturnNullIfEmpty(),
                        SerialNumber = worksheet.Cell(row, 5).GetString().ReturnNullIfEmpty(),
                        Vendor = !worksheet.Cell(row, 7).GetString().IsEmpty() ? await _entityChecker.GetOrCreateVendorAsync(worksheet.Cell(row, 7).GetString()) : null,
                        Cabinet = !worksheet.Cell(row, 9).GetString().IsEmpty() ? await _entityChecker.GetOrCreateCabinetAsync(worksheet.Cell(row, 9).GetString()) : null,
                        CabinetEquipmentType = await _entityChecker.GetOrCreateCabinetEquipmentTypeAsync(worksheet.Cell(row, 10).GetString())
                    };
                    equipments.Add(equipment);
                }
                return equipments;
            }
        }
    }
}
