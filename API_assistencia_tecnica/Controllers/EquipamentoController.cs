﻿using Microsoft.AspNetCore.Mvc;
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
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound("Equipamento não encontrado.");
            return Ok(item);
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

        // ✅ DELETE MELHORADO - Com tratamento de erro
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sucesso = await _service.DeleteAsync(id);
                if (!sucesso)
                    return NotFound("Equipamento não encontrado.");
                return NoContent();
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
