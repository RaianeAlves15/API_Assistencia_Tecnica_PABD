using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Services;
using API_assistencia_tecnica.Dtos;

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
        public async Task<IActionResult> Create([FromBody] FornecedorDto dto)
        {
            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = novo.IdFornecedor }, novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FornecedorDto dto)
        {
            var atualizado = await _service.UpdateAsync(id, dto);
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
