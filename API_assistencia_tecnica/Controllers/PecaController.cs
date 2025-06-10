using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;
using API_assistencia_tecnica.Dtos;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PecaController : ControllerBase
    {
        private readonly PecaService _service;

        public PecaController(PecaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pecas = await _service.GetAllAsync();
            return Ok(pecas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var peca = await _service.GetByIdAsync(id);
            if (peca == null)
                return NotFound("Peça não encontrada.");
            return Ok(peca);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PecaDto dto)
        {
            var nova = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nova.Id }, nova);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PecaDto dto)
        {
            var atualizada = await _service.UpdateAsync(id, dto);
            if (atualizada == null)
                return NotFound("Peça não encontrada.");
            return Ok(atualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Peça não encontrada.");
            return NoContent();
        }
    }
}
