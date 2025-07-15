using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReparoPecaController : ControllerBase
{
    private readonly ReparoPecaService _service;

    public ReparoPecaController(ReparoPecaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _service.GetAllAsync();
        return Ok(lista);
    }

    [HttpGet("{reparoId}/{pecaId}")]
    public async Task<IActionResult> GetById(int reparoId, int pecaId)
    {
        var item = await _service.GetByIdAsync(reparoId, pecaId);
        if (item == null)
            return NotFound("Relação reparo-peça não encontrada.");
        return Ok(item);
    }

    [HttpGet("reparo/{reparoId}")]
    public async Task<IActionResult> GetPecasByReparo(int reparoId)
    {
        var lista = await _service.GetPecasByReparoIdAsync(reparoId);
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReparoPecaDto dto)
    {
        try
        {
            var existe = await _service.ExistsAsync(dto.ReparoId, dto.PecaId);
            if (existe)
                return Conflict("Esta relação reparo-peça já existe.");

            var novo = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { reparoId = novo.ReparoId, pecaId = novo.PecaId }, novo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{reparoId}/{pecaId}")]
    public async Task<IActionResult> Update(int reparoId, int pecaId, [FromBody] ReparoPecaDto dto)
    {
        dto.ReparoId = reparoId;
        dto.PecaId = pecaId;

        var atualizado = await _service.UpdateAsync(reparoId, pecaId, dto);
        if (atualizado == null)
            return NotFound("Relação reparo-peça não encontrada.");
        return Ok(atualizado);
    }

    [HttpDelete("{reparoId}/{pecaId}")]
    public async Task<IActionResult> Delete(int reparoId, int pecaId)
    {
        var sucesso = await _service.DeleteAsync(reparoId, pecaId);
        if (!sucesso)
            return NotFound("Relação reparo-peça não encontrada.");
        return NoContent();
    }

   
}