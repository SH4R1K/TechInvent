using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using TechInvent.BLL.Dto;
using TechInvent.BLL.DtoModels;
using TechInvent.DM.Models;
using TechInvent.DAL.Data;

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
        public IActionResult Post([FromBody] CabinetDto cabinetDto)
        {
            try
            {
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
