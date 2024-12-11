using ClosedXML.Excel;
using WebMVC.Models;
namespace WebMVC.Services
{
    public class ExcelService
    {
        public async Task<MemoryStream> GenerateCabinetsReportAsync(List<Cabinet> cabinets)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                foreach (var cabinet in cabinets)
                {
                    var workplaces = cabinet.Workplaces.ToList();
                    var worksheet = workbook.Worksheets.Add(cabinet.Name ?? "Безымянный");
                    GenerateCabinetWorksheet(worksheet, cabinet);

                }
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<MemoryStream> GenerateCabinetReportAsync(Cabinet cabinet)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                var workplaces = cabinet.Workplaces.ToList();
                var worksheet = workbook.Worksheets.Add(cabinet.Name ?? "Безымянный");
                GenerateCabinetWorksheet(worksheet, cabinet);

                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;

            return memoryStream;
        }
        public async Task<MemoryStream> GenerateWorkplaceReportAsync(Workplace workplace)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(workplace.Name ?? "Безымянный");
                GenerateWorkplaceHardwareWorksheet(worksheet, workplace);
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
        public async Task<MemoryStream> GenerateWorkplacesReportAsync(List<Workplace> workplaces)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                foreach (var workplace in workplaces)
                {
                    var worksheet = workbook.Worksheets.Add(workplace.Name ?? "Безымянный");
                    GenerateWorkplaceHardwareWorksheet(worksheet, workplace);

                }
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        private void GenerateCabinetWorksheet(IXLWorksheet worksheet, Cabinet cabinet)
        {
            var workplaces = cabinet.Workplaces.ToList();
            worksheet.Cell(1, 1).Value = "Количество рабочих мест";
            worksheet.Cell(1, 2).Value = workplaces.Count;

            worksheet.Cell(4, 1).Value = "№";
            worksheet.Cell(4, 2).Value = "Название рабочего места";
            worksheet.Cell(4, 3).Value = "Операционная система";
            worksheet.Cell(4, 4).Value = "Версия операционной системы";
            worksheet.Cell(4, 5).Value = "Последнее обновление";
            worksheet.Cell(4, 6).Value = "Уникальный идентификатор";

            for (int i = 0; i < workplaces.Count; i++)
            {
                worksheet.Cell(5 + i, 1).Value = i + 1;
                worksheet.Cell(5 + i, 2).Value = workplaces[i].Name;
                worksheet.Cell(5 + i, 3).Value = workplaces[i].IdOsNavigation.OsName;
                worksheet.Cell(5 + i, 4).Value = workplaces[i].IdOsNavigation.OsVersion;
                worksheet.Cell(5 + i, 5).Value = workplaces[i].LastUpdate;
                worksheet.Cell(5 + i, 6).Value = workplaces[i].Guid.ToString();
            }
            worksheet.Columns().AdjustToContents();
        }
        private void GenerateWorkplaceHardwareWorksheet(IXLWorksheet worksheet, Workplace workplace)
        {
            int addedComponents = 0;
            worksheet.Cell(1, 1).Value = "Название рабочего места";
            worksheet.Cell(1, 2).Value = workplace.Name;

            worksheet.Cell(2, 1).Value = "Операционная система";
            worksheet.Cell(2, 2).Value = workplace.IdOsNavigation.OsName;

            worksheet.Cell(3, 1).Value = "Материнская плата";
            worksheet.Cell(3, 2).Value = workplace.Components.FirstOrDefault(c => c.Mainboard != null)?.Name;

            worksheet.Cell(4, 1).Value = "Процессор";
            worksheet.Cell(4, 2).Value = workplace.Components.FirstOrDefault(c => c.Processor != null)?.Name;

            worksheet.Cell(5, 1).Value = "Видеокарта";
            var gpus = workplace.Components.Where(c => c.Gpu != null).ToList();
            foreach (var gpu in gpus)
            {
                worksheet.Cell(5 + addedComponents, 2).Value = gpu.Name;
                addedComponents++;
            }
            if (gpus.Count() > 0)
                addedComponents--;

            worksheet.Cell(6 + addedComponents, 1).Value = "Оперативная память";
            var rams = workplace.Components.Where(c => c.Ram != null).ToList();
            foreach (Ram ram in rams)
            {
                worksheet.Cell(6 + addedComponents, 2).Value = $"{ram.Name} - {ram.Capacity}";
                addedComponents++;
            }
            if (rams.Count() > 0)
                addedComponents--;

            worksheet.Cell(7 + addedComponents, 1).Value = "Сетевые адаптеры";
            var netadapters = workplace.Components.Where(c => c.NetAdapter != null).ToList();
            foreach (var netadapter in netadapters)
            {
                worksheet.Cell(7 + addedComponents, 2).Value = netadapter.Name;
                addedComponents++;
            }
            if (netadapters.Count() > 0)
                addedComponents--;

            worksheet.Cell(8 + addedComponents, 1).Value = "Диски";
            var disks = workplace.Components.Where(c => c.Disk != null).ToList();
            foreach (Disk disk in disks)
            {
                worksheet.Cell(8 + addedComponents, 2).Value = $"{disk.Model} - {disk.Size}Гб";
                addedComponents++;
            }

            worksheet.Columns().AdjustToContents();
        }

    }
}
