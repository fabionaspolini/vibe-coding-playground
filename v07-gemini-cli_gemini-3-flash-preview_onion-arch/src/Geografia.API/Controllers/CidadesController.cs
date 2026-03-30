using Geografia.Application.Dtos;
using Geografia.Application.Services;
using Geografia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Geografia.API.Controllers;

[ApiController]
[Route("cidades")]
public class CidadesController(CidadeService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CidadeDto>> Create(CidadeRequest request)
    {
        var result = await service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CidadeDto>> GetById(Guid id)
    {
        var result = await service.GetById(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CidadeDto>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? estadoId,
        [FromQuery] bool? ativo)
    {
        Expression<Func<Cidade, bool>>? filter = null;
        if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(estadoId) || ativo.HasValue)
        {
            filter = c =>
                (string.IsNullOrEmpty(nome) || c.Nome.Contains(nome)) &&
                (string.IsNullOrEmpty(estadoId) || c.EstadoId == estadoId) &&
                (!ativo.HasValue || c.Ativo == ativo.Value);
        }
        return Ok(await service.List(filter));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CidadeDto>> Update(Guid id, CidadeRequest request)
    {
        var result = await service.Update(id, request);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var result = await service.Remove(id);
        return result ? NoContent() : NotFound();
    }
}
