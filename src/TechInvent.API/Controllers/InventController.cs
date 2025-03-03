using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces;

namespace TechInvent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventController : ControllerBase
    {
        private readonly IInventService _inventService;

        public InventController(IInventService inventService)
        {
            _inventService = inventService;
        }

        [HttpPost(Name = "PostInvent")]
        public async Task<IActionResult> PostAsync([FromBody] InventCabinetDto cabinetDto)
        {
            //try
            //{

                return Ok(await _inventService.InventWorkplace(cabinetDto));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return StatusCode(500);
            //}
        }
    }
}
