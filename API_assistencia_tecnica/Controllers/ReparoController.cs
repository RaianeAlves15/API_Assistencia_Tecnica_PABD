using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;
using API_assistencia_tecnica.Dtos;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparoController : ControllerBase
    {
        private readonly ReparoService _service;

        public ReparoController(ReparoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reparos = await _service.GetAllAsync();
            return Ok(reparos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reparo = await _service.GetByIdAsync(id);
            if (reparo == null)
                return NotFound("Reparo não encontrado.");
            return Ok(reparo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReparoDto dto)
        {
            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = novo.IdLancamentoReparo }, novo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Reparo não encontrado.");
            return NoContent();
        }
    }
}
