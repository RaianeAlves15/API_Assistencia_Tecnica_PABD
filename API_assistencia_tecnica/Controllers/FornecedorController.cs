using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly FornecedorService _service;

        public FornecedorController(FornecedorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores = await _service.GetAllAsync();
            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fornecedor = await _service.GetByIdAsync(id);
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado.");
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Fornecedor fornecedor)
        {
            var novo = await _service.CreateAsync(fornecedor);
            return CreatedAtAction(nameof(GetById), new { id = novo.IdFornecedor }, novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Fornecedor fornecedor)
        {
            var atualizado = await _service.UpdateAsync(id, fornecedor);
            if (atualizado == null)
                return NotFound("Fornecedor não encontrado.");
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Fornecedor não encontrado.");
            return NoContent();
        }
    }
}