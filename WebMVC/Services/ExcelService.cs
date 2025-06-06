using ClosedXML.Excel;
using TechInvent.DM.Models;
namespace WebMVC.Services
{
    public partial class ExcelService
    {
        public async Task<MemoryStream> GenerateCabinetsReportAsync(List<Cabinet> cabinets)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                foreach (var cabinet in cabinets)
                {
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
                var worksheet = workbook.Worksheets.Add(cabinet.Name ?? "Безымянный");
                GenerateCabinetWorksheet(worksheet, cabinet);

                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;

            return memoryStream;
        }
        public async Task<MemoryStream> GenerateWorkplaceSoftwareReportAsync(Workplace workplace)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(workplace.Name ?? "Безымянный");
                GenerateWorkplaceSoftwareWorksheet(worksheet, workplace);
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
        public async Task<MemoryStream> GenerateWorkplacesSoftwareReportAsync(List<Workplace> workplaces)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                foreach (var workplace in workplaces)
                {
                    var worksheet = workbook.Worksheets.Add(workplace.Name ?? "Безымянный");
                    GenerateWorkplaceSoftwareWorksheet(worksheet, workplace);

                }
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
        public async Task<MemoryStream> GenerateWorkplaceHardwareReportAsync(Workplace workplace)
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
        public async Task<MemoryStream> GenerateCabinetWorkplacesHardwareReportAsync(List<Workplace> workplaces)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(workplaces.FirstOrDefault()?.IdCabinetNavigation.Name ?? "Безымянный");
                GenerateCabinetWorkplacesHardwareWorksheet(worksheet, workplaces.OrderBy(w => w.Name).ToList());
                await Task.Run(() => workbook.SaveAs(memoryStream));
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
        public async Task<MemoryStream> GenerateCabinetWorkplacesSoftwareReportAsync(List<Workplace> workplaces, List<Software> softwares)
        {
            var memoryStream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(workplaces.FirstOrDefault()?.IdCabinetNavigation.Name ?? "Безымянный");
                GenerateCabinetWorkplacesSoftwareWorksheet(worksheet, workplaces.OrderBy(w => w.Name).ToList(), softwares);
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

            workplaces = workplaces.OrderBy(w => w.Name).ToList();
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
        private void GenerateCabinetWorkplacesSoftwareWorksheet(IXLWorksheet worksheet, List<Workplace> workplaces, List<Software> softwares)
        {

            for (int i = 0; i < workplaces.Count; i++)
            {
                worksheet.Cell(1, 1).Value = "Рабочее место";
                worksheet.Cell(2, 1).Value = "Количество ПО";
                worksheet.Cell(1, 3 + i).Value = workplaces[i].Name;
                worksheet.Cell(2, 3 + i).Value = workplaces[i].InstalledSoftware.Count;
                for (int j = 0; j < softwares.Count; j++)
                {
                    if (i == 0)
                    {
                        worksheet.Cell(3 + j, 1).Value = softwares[j].Name;
                        worksheet.Cell(3 + j, 2).Value = softwares[j].Version;
                    }
                    if (workplaces[i].InstalledSoftware.Any(s => s.IdSoftware == softwares[j].IdSoftware))
                    {
                        worksheet.Cell(3 + j, 3 + i).Value = "+";
                    }
                }
            }

            worksheet.Rows().AdjustToContents();
            worksheet.Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            worksheet.Columns().AdjustToContents();
        }

        private void GenerateCabinetWorkplacesHardwareWorksheet(IXLWorksheet worksheet, List<Workplace> workplaces)
        {
            worksheet.Cell(1, 1).Value = "Количество рабочих мест";
            worksheet.Cell(1, 2).Value = workplaces.Count;

            worksheet.Cell(4, 1).Value = "№";
            worksheet.Cell(4, 2).Value = "Название рабочего места";
            worksheet.Cell(4, 3).Value = "Операционная система";
            worksheet.Cell(4, 4).Value = "Материнская плата";
            worksheet.Cell(4, 5).Value = "Процессор";
            worksheet.Cell(4, 6).Value = "Видеокарта";
            worksheet.Cell(4, 7).Value = "Оперативная память";
            worksheet.Cell(4, 8).Value = "Сетевые адаптеры";
            worksheet.Cell(4, 9).Value = "MAC";
            worksheet.Cell(4, 10).Value = "Диски";
            worksheet.Cell(4, 11).Value = "Оборудование";

            for (int i = 0; i < workplaces.Count; i++)
            {
                worksheet.Cell(5 + i, 1).Value = i + 1;
                worksheet.Cell(5 + i, 2).Value = workplaces[i].Name;
                worksheet.Cell(5 + i, 3).Value = workplaces[i].IdOsNavigation.OsName;
                worksheet.Cell(5 + i, 4).Value = workplaces[i].Components.FirstOrDefault(c => c.Mainboard != null)?.Name;
                worksheet.Cell(5 + i, 5).Value = workplaces[i].Components.FirstOrDefault(c => c.Processor != null)?.Name;
                worksheet.Cell(5 + i, 6).Value = String.Join(Environment.NewLine, workplaces[i].Components.Where(c => c.Gpu != null).Select(g => $"{g.Name}").ToList());
                worksheet.Cell(5 + i, 7).Value = String.Join(Environment.NewLine, workplaces[i].Components.Where(c => c.Ram != null).Select(r => $"{r.Name} - {r.Ram.Capacity}").ToList());
                worksheet.Cell(5 + i, 8).Value = String.Join(Environment.NewLine, workplaces[i].Components.Where(c => c.NetAdapter != null).Select(n => $"{n.Name}").ToList());
                worksheet.Cell(5 + i, 9).Value = String.Join(Environment.NewLine, workplaces[i].Components.Where(c => c.NetAdapter != null).Select(n => $"{n.NetAdapter.MacAddress}").ToList());
                worksheet.Cell(5 + i, 10).Value = String.Join(Environment.NewLine, workplaces[i].Components.Where(c => c.Disk != null).Select(d => $"{d.Name} - {d.Disk.Size}Гб").ToList());
                worksheet.Cell(5 + i, 11).Value = String.Join(Environment.NewLine, workplaces[i].CabinetEquipments.Select(d => $"{d.Name}({d.InventNumber ?? " - "})").ToList());
            }
            worksheet.Rows().AdjustToContents();
            worksheet.Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            worksheet.Columns().AdjustToContents();
        }
        private void GenerateWorkplaceHardwareWorksheet(IXLWorksheet worksheet, Workplace workplace)
        {
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
                worksheet.Cell(5, 2).Value += $"{gpu.Name}{Environment.NewLine}";
            }

            worksheet.Cell(6, 1).Value = "Оперативная память";
            var rams = workplace.Components.Where(c => c.Ram != null).ToList();
            foreach (Ram ram in rams)
            {
                worksheet.Cell(6, 2).Value += $"{ram.Name} - {ram.Capacity}{Environment.NewLine}";
            }

            worksheet.Cell(7, 1).Value = "Сетевые адаптеры";
            var netadapters = workplace.Components.Where(c => c.NetAdapter != null).ToList();
            foreach (NetAdapter netadapter in netadapters)
            {
                worksheet.Cell(7, 2).Value += $"{netadapter.Name}{Environment.NewLine}";
                worksheet.Cell(7, 3).Value += $"{netadapter.MacAddress}{Environment.NewLine}";
            }

            worksheet.Cell(8, 1).Value = "Диски";
            var disks = workplace.Components.Where(c => c.Disk != null).ToList();
            foreach (Disk disk in disks)
            {
                worksheet.Cell(8, 2).Value += $"{disk.Name} - {disk.Size}Гб{Environment.NewLine}";
            }
            worksheet.Cell(9, 1).Value = "Оборудование";
            var equipments = workplace.CabinetEquipments.ToList();
            foreach (CabinetEquipment equipment in equipments)
            {
                worksheet.Cell(9, 2).Value += $"{equipment.Name}({equipment.InventNumber ?? "-"}){Environment.NewLine}";
            }
            worksheet.Rows().AdjustToContents();
            worksheet.Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            worksheet.Columns().AdjustToContents();
        }
        private void GenerateWorkplaceSoftwareWorksheet(IXLWorksheet worksheet, Workplace workplace)
        {
            int addedComponents = 0;
            worksheet.Cell(1, 1).Value = "Название рабочего места";
            worksheet.Cell(1, 2).Value = workplace.Name;

            worksheet.Cell(2, 1).Value = "Операционная система";
            worksheet.Cell(2, 2).Value = workplace.IdOsNavigation.OsName;
            worksheet.Cell(3, 1).Value = "Количество установленных программ";
            worksheet.Cell(3, 2).Value = workplace.InstalledSoftware.Count;

            var groups = workplace.InstalledSoftware.GroupBy(s => s.SoftwareNavigation.IdManufacturer);
            foreach (var group in groups)
            {
                worksheet.Cell(4 + addedComponents, 1).Value = group.FirstOrDefault()?.SoftwareNavigation.ManufacturerNavigation.Name;
                foreach (var software in group)
                {
                    worksheet.Cell(4 + addedComponents, 2).Value = software.SoftwareNavigation?.Name;
                    addedComponents++;
                }
                addedComponents--;
            }
            worksheet.Columns().AdjustToContents();
        }

    }
}
