using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorPecaController : ControllerBase
    {
        private readonly FornecedorPecasService _service;

        public FornecedorPecaController(FornecedorPecasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{fornecedorId}/{pecaId}")]
        public async Task<IActionResult> GetById(int fornecedorId, int pecaId)
        {
            var item = await _service.GetByIdAsync(fornecedorId, pecaId);
            if (item == null)
                return NotFound("Associação Fornecedor-Peça não encontrada.");
            return Ok(item);
        }

        [HttpGet("fornecedor/{fornecedorId}")]
        public async Task<IActionResult> GetPecasByFornecedor(int fornecedorId)
        {
            var lista = await _service.GetPecasByFornecedorIdAsync(fornecedorId);
            return Ok(lista);
        }

        [HttpGet("peca/{pecaId}")]
        public async Task<IActionResult> GetFornecedoresByPeca(int pecaId)
        {
            var lista = await _service.GetFornecedoresByPecaIdAsync(pecaId);
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FornecedorPecaDto dto)
        {
            // Verificar se já existe a relação
            var existe = await _service.ExistsAsync(dto.FornecedorId, dto.PecaId);
            if (existe)
                return Conflict("Esta associação fornecedor-peça já existe.");

            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { fornecedorId = novo.FornecedorId, pecaId = novo.PecaId }, novo);
        }

        [HttpPut("{fornecedorId}/{pecaId}")]
        public async Task<IActionResult> Update(int fornecedorId, int pecaId, [FromBody] FornecedorPecaDto dto)
        {
            // Garantir que os IDs do DTO correspondem aos da rota
            dto.FornecedorId = fornecedorId;
            dto.PecaId = pecaId;

            var atualizado = await _service.UpdateAsync(fornecedorId, pecaId, dto);
            if (atualizado == null)
                return NotFound("Associação Fornecedor-Peça não encontrada.");
            return Ok(atualizado);
        }

        [HttpDelete("{fornecedorId}/{pecaId}")]
        public async Task<IActionResult> Delete(int fornecedorId, int pecaId)
        {
            var sucesso = await _service.DeleteAsync(fornecedorId, pecaId);
            if (!sucesso)
                return NotFound("Associação Fornecedor-Peça não encontrada.");
            return NoContent();
        }
    }
}