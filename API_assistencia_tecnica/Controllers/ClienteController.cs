using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _service.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            var novo = await _service.CreateAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            var atualizado = await _service.UpdateAsync(id, cliente);
            if (atualizado == null)
                return NotFound("Cliente não encontrado.");
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Cliente não encontrado.");
            return NoContent();
        }
    }
}