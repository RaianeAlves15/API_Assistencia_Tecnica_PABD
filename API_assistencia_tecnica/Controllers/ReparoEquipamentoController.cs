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
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{reparoId}/{equipamentoId}")]
        public async Task<IActionResult> GetById(int reparoId, int equipamentoId)
        {
            var item = await _service.GetByIdAsync(reparoId, equipamentoId);
            if (item == null)
                return NotFound("Relação reparo-equipamento não encontrada.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReparoEquipamentoDto dto)
        {
            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { reparoId = novo.ReparoId, equipamentoId = novo.EquipamentoId }, novo);
        }

        [HttpPut("{reparoId}/{equipamentoId}")]
        public async Task<IActionResult> Update(int reparoId, int equipamentoId, [FromBody] ReparoEquipamentoDto dto)
        {
            var atualizado = await _service.UpdateAsync(reparoId, equipamentoId, dto);
            if (atualizado == null)
                return NotFound("Relação reparo-equipamento não encontrada.");
            return Ok(atualizado);
        }

        [HttpDelete("{reparoId}/{equipamentoId}")]
        public async Task<IActionResult> Delete(int reparoId, int equipamentoId)
        {
            var sucesso = await _service.DeleteAsync(reparoId, equipamentoId);
            if (!sucesso)
                return NotFound("Relação reparo-equipamento não encontrada.");
            return NoContent();
        }
    }
}