using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TechInvent.BLL.Dto;
using TechInvent.BLL.DtoModels;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImportController : Controller
    {
        private readonly DtoConverter _dtoConverter;

        public ImportController(DtoConverter dtoConverter)
        {
            _dtoConverter = dtoConverter;
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
                            _dtoConverter.ConvertDtoCabinet(cabinetDto);
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
    }
}
