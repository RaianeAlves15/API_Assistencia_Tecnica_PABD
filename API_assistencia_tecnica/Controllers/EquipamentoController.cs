using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;
using API_assistencia_tecnica.Dtos;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {
        private readonly EquipamentoService _service;

        public EquipamentoController(EquipamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipamentos = await _service.GetAllAsync();
            return Ok(equipamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var equipamento = await _service.GetByIdAsync(id);
            if (equipamento == null)
                return NotFound("Equipamento não encontrado.");
            return Ok(equipamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EquipamentoDto dto)
        {
            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipamentoDto dto)
        {
            var atualizado = await _service.UpdateAsync(id, dto);
            if (atualizado == null)
                return NotFound("Equipamento não encontrado.");
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Equipamento não encontrado.");
            return NoContent();
        }
    }
}
