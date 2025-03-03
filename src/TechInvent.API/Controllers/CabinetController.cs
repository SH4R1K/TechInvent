using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces;

namespace TechInvent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {
        private readonly ICabinetService _cabinetService;
        public CabinetController(ICabinetService cabinetService)
        {
            _cabinetService = cabinetService;
        }

        [HttpGet]
        public async Task<List<CabinetDto>> Get()
        {
            return await _cabinetService.GetAllAsync();
        }
    }
}
