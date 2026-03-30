using Geografia.Application.Dtos;
using Geografia.Application.Services;
using Geografia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Geografia.API.Controllers;

[ApiController]
[Route("estados")]
public class EstadosController(EstadoService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EstadoDto>> Create(EstadoRequest request)
    {
        var result = await service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoDto>> GetById(string id)
    {
        var result = await service.GetById(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoDto>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? sigla,
        [FromQuery] string? paisId,
        [FromQuery] bool? ativo)
    {
        Expression<Func<Estado, bool>>? filter = null;
        if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(sigla) || !string.IsNullOrEmpty(paisId) || ativo.HasValue)
        {
            filter = e =>
                (string.IsNullOrEmpty(nome) || e.Nome.Contains(nome)) &&
                (string.IsNullOrEmpty(sigla) || e.Sigla == sigla) &&
                (string.IsNullOrEmpty(paisId) || e.PaisId == paisId) &&
                (!ativo.HasValue || e.Ativo == ativo.Value);
        }
        return Ok(await service.List(filter));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EstadoDto>> Update(string id, EstadoRequest request)
    {
        var result = await service.Update(id, request);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        var result = await service.Remove(id);
        return result ? NoContent() : NotFound();
    }
}
