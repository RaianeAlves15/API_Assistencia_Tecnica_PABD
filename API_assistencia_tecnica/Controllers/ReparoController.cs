using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReparoController : ControllerBase
{
    private readonly ReparoService _service;

    public ReparoController(ReparoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReparoDto>>> Get()
    {
        var reparos = await _service.GetAllAsync();
        return Ok(reparos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReparoDto>> GetById(int id)
    {
        var reparo = await _service.GetByIdAsync(id);
        return reparo == null ? NotFound() : Ok(reparo);
    }

    [HttpPost]
    public async Task<ActionResult<ReparoDto>> Post([FromBody] ReparoCreateDto dto)
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
    public async Task<ActionResult<ReparoDto>> Put(int id, [FromBody] ReparoCreateDto dto)
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
    public async Task<ActionResult<List<ReparoDto>>> GetByCliente(int clienteId)
    {
        var reparos = await _service.GetByClienteIdAsync(clienteId);
        return Ok(reparos);
    }

    [HttpPost("from-orcamento/{orcamentoId}")]
    public async Task<ActionResult<ReparoDto>> CreateFromOrcamento(int orcamentoId)
    {
        try
        {
            var reparo = await _service.CreateFromOrcamentoAsync(orcamentoId);
            return CreatedAtAction(nameof(GetById), new { id = reparo.Id }, reparo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}