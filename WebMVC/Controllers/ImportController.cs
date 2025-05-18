using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TechInvent.BLL.Dto;
using TechInvent.BLL.DtoModels;
using TechInvent.DAL.Data;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImportController : Controller
    {
        private readonly DtoConverter _dtoConverter;
        private readonly ExcelService _excelService;
        private readonly TechInventContext _context;

        public ImportController(DtoConverter dtoConverter, ExcelService excelService, TechInventContext context)
        {
            _dtoConverter = dtoConverter;
            _excelService = excelService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAndJson([FromForm] List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    try
                    {
                        using (var stream = new StreamReader(file.OpenReadStream()))
                        {
                            var json = await stream.ReadToEndAsync();
                            var cabinetDto = JsonSerializer.Deserialize<CabinetDto>(json);
                            await _dtoConverter.ConvertDtoCabinetAsync(cabinetDto);
                            Console.WriteLine("Cabinet updated");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return RedirectToAction("Index","Cabinets");
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileAndExcel([FromForm] List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    try
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            stream.Position = 0; 

                            var equipments = await _excelService.ImportEquipmentAsync(stream);

                            var existingEquipments = await _context.CabinetEquipments
                                .Select(e => new { e.SerialNumber, e.InventNumber })
                                .ToListAsync();


                            var newEquipments = equipments
                                .Where(e => !existingEquipments
                                    .Any(existing =>
                                        existing.SerialNumber == e.SerialNumber ||
                                        existing.InventNumber == e.InventNumber))
                                .ToList();

                            if (newEquipments.Any())
                            {
                                await _context.CabinetEquipments.AddRangeAsync(newEquipments);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return RedirectToAction("Index", "Cabinets");
        }
    }
}
