using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentoPecaController : ControllerBase
    {
        private readonly OrcamentoPecaService _service;

        public OrcamentoPecaController(OrcamentoPecaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{orcamentoId}/{pecaId}")]
        public async Task<IActionResult> GetById(int orcamentoId, int pecaId)
        {
            var item = await _service.GetByIdAsync(orcamentoId, pecaId);
            if (item == null)
                return NotFound("Relação orçamento-peça não encontrada.");
            return Ok(item);
        }

        [HttpGet("orcamento/{orcamentoId}")]
        public async Task<IActionResult> GetPecasByOrcamento(int orcamentoId)
        {
            var lista = await _service.GetPecasByOrcamentoIdAsync(orcamentoId);
            return Ok(lista);
        }

        [HttpGet("peca/{pecaId}")]
        public async Task<IActionResult> GetOrcamentosByPeca(int pecaId)
        {
            var lista = await _service.GetOrcamentosByPecaIdAsync(pecaId);
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrcamentoPecaDto dto)
        {
            // Verificar se já existe a relação
            var existe = await _service.ExistsAsync(dto.OrcamentoId, dto.PecaId);
            if (existe)
                return Conflict("Esta relação orçamento-peça já existe.");

            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { orcamentoId = novo.OrcamentoId, pecaId = novo.PecaId }, novo);
        }

        [HttpPut("{orcamentoId}/{pecaId}")]
        public async Task<IActionResult> Update(int orcamentoId, int pecaId, [FromBody] OrcamentoPecaDto dto)
        {
            // Garantir que os IDs do DTO correspondem aos da rota
            dto.OrcamentoId = orcamentoId;
            dto.PecaId = pecaId;

            var atualizado = await _service.UpdateAsync(orcamentoId, pecaId, dto);
            if (atualizado == null)
                return NotFound("Relação orçamento-peça não encontrada.");
            return Ok(atualizado);
        }

        [HttpDelete("{orcamentoId}/{pecaId}")]
        public async Task<IActionResult> Delete(int orcamentoId, int pecaId)
        {
            var sucesso = await _service.DeleteAsync(orcamentoId, pecaId);
            if (!sucesso)
                return NotFound("Relação orçamento-peça não encontrada.");
            return NoContent();
        }
    }
}