using Microsoft.AspNetCore.Mvc;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Services;

namespace API_assistencia_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorPecaController : ControllerBase
    {
        private readonly FornecedorPecasService _service;

        public FornecedorPecaController(FornecedorPecasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FornecedorPecaDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorPecaDto>> Post([FromBody] FornecedorPecaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { }, created);
        }
    }
}
