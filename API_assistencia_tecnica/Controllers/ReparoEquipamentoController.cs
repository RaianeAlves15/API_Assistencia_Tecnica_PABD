using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReparoEquipamentoController : ControllerBase
    {
        private readonly ReparoEquipamentoService _service;

        public ReparoEquipamentoController(ReparoEquipamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReparoEquipamentoDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ReparoEquipamentoDto>> Post([FromBody] ReparoEquipamentoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { }, created);
        }
    }
}

