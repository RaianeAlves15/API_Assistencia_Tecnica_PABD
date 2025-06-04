using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;

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
        public async Task<IActionResult> Create([FromBody] Equipamento equipamento)
        {
            var novo = await _service.CreateAsync(equipamento);
            return CreatedAtAction(nameof(GetById), new { id = novo.IdEquipamento }, novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Equipamento equipamento)
        {
            var atualizado = await _service.UpdateAsync(id, equipamento);
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