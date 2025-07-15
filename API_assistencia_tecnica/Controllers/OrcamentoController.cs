using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrcamentoController : ControllerBase
{
    private readonly OrcamentoService _service;

    public OrcamentoController(OrcamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrcamentoDto>>> Get()
    {
        var orcamentos = await _service.GetAllAsync();
        return Ok(orcamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrcamentoDto>> GetById(int id)
    {
        var orcamento = await _service.GetByIdAsync(id);
        return orcamento == null ? NotFound() : Ok(orcamento);
    }

    [HttpPost]
    public async Task<ActionResult<OrcamentoDto>> Post([FromBody] OrcamentoCreateDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrcamentoDto>> Put(int id, [FromBody] OrcamentoCreateDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // ✅ DELETE MELHORADO
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
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

    // ✅ NOVO ENDPOINT
    [HttpGet("{id}/can-delete")]
    public async Task<IActionResult> CanDelete(int id)
    {
        var (canDelete, reason) = await _service.CanDeleteAsync(id);
        return Ok(new { canDelete, reason });
    }


    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<List<OrcamentoDto>>> GetByCliente(int clienteId)
    {
        var orcamentos = await _service.GetByClienteIdAsync(clienteId);
        return Ok(orcamentos);
    }
}
