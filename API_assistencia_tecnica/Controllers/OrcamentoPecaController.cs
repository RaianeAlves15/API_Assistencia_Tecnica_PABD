using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentoPecaController : ControllerBase
    {
        private readonly OrcamentoPecaService _service;

        public OrcamentoPecaController(OrcamentoPecaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrcamentoPecaDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoPecaDto>> Post([FromBody] OrcamentoPecaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { }, created);
        }
    }
}
