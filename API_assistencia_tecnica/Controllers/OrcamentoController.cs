using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentoController : ControllerBase
    {
        private readonly OrcamentoService _service;

        public OrcamentoController(OrcamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrcamentoDto>>> Get()
        {
            var orcamentos = await _service.GetAllAsync();
            return Ok(orcamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrcamentoDto>> GetById(int id)
        {
            var orcamento = await _service.GetByIdAsync(id);
            return orcamento == null ? NotFound() : Ok(orcamento);
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoDto>> Post([FromBody] OrcamentoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrcamentoDto>> Put(int id, [FromBody] OrcamentoDto dto)
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
