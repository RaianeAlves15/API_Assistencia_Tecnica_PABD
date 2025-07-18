﻿using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PecaController : ControllerBase
    {
        private readonly PecaService _service;

        public PecaController(PecaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PecaDto>>> Get()
        {
            var pecas = await _service.GetAllAsync();
            return Ok(pecas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PecaDto>> GetById(int id)
        {
            var peca = await _service.GetByIdAsync(id);
            return peca == null ? NotFound("Peça não encontrada.") : Ok(peca);
        }

        [HttpPost]
        public async Task<ActionResult<PecaDto>> Post([FromBody] PecaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PecaDto>> Put(int id, [FromBody] PecaDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound("Peça não encontrada.") : Ok(updated);
        }

        // ✅ DELETE MELHORADO - Com tratamento de erro
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                return deleted ? NoContent() : NotFound("Peça não encontrada.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = "Não foi possível excluir",
                    message = ex.Message
                });
            }
        }

        // ✅ NOVO ENDPOINT - Verificar se pode deletar
        [HttpGet("{id}/can-delete")]
        public async Task<IActionResult> CanDelete(int id)
        {
            var (canDelete, reason) = await _service.CanDeleteAsync(id);
            return Ok(new
            {
                canDelete = canDelete,
                reason = reason
            });
        }
    }
}