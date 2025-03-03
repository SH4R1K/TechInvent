using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using TechInventAPI.Data;
using TechInventAPI.Dto;
using TechInventAPI.DtoModels;
using TechInvent.DM.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechInventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventController : ControllerBase
    {
        private readonly TechInventContext _context;
        private readonly DtoConverter _dtoConverter;
        public InventController(TechInventContext context, DtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }
        [HttpPost(Name = "PostInvent")]
        public IActionResult Post(object json)
        {
            try
            {
                var cabinetDto = JsonConvert.DeserializeObject<CabinetDto>(json.ToString());
                _dtoConverter.ConvertDtoCabinet(cabinetDto);
                Console.WriteLine("Cabinet updated");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
