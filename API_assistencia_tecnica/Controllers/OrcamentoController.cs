using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {
        private readonly OrcamentoService _service;

        public OrcamentoController(OrcamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orcamentos = await _service.GetAllAsync();
            return Ok(orcamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orcamento = await _service.GetByIdAsync(id);
            if (orcamento == null)
                return NotFound("Orçamento não encontrado.");
            return Ok(orcamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Orcamento orcamento)
        {
            var novo = await _service.CreateAsync(orcamento);
            return CreatedAtAction(nameof(GetById), new { id = novo.IdOrcamento }, novo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Orçamento não encontrado.");
            return NoContent();
        }
    }
}