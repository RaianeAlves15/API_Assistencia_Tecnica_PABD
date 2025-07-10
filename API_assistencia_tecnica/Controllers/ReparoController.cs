using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReparoController : ControllerBase
    {
        private readonly ReparoService _service;

        public ReparoController(ReparoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReparoDto>>> Get()
        {
            var reparos = await _service.GetAllAsync();
            return Ok(reparos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReparoDto>> GetById(int id)
        {
            var reparo = await _service.GetByIdAsync(id);
            return reparo == null ? NotFound() : Ok(reparo);
        }

        [HttpPost]
        public async Task<ActionResult<ReparoDto>> Post([FromBody] ReparoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReparoDto>> Put(int id, [FromBody] ReparoDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
