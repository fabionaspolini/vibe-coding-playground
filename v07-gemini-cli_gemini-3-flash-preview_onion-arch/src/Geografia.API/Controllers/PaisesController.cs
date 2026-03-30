using Geografia.Application.Dtos;
using Geografia.Application.Services;
using Geografia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Geografia.API.Controllers;

[ApiController]
[Route("paises")]
public class PaisesController(PaisService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PaisDto>> Create(PaisRequest request)
    {
        var result = await service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaisDto>> GetById(string id)
    {
        var result = await service.GetById(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisDto>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? codigoISO3,
        [FromQuery] bool? ativo)
    {
        Expression<Func<Pais, bool>>? filter = null;
        if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(codigoISO3) || ativo.HasValue)
        {
            filter = p => 
                (string.IsNullOrEmpty(nome) || p.Nome.Contains(nome)) &&
                (string.IsNullOrEmpty(codigoISO3) || p.CodigoISO3 == codigoISO3) &&
                (!ativo.HasValue || p.Ativo == ativo.Value);
        }
        return Ok(await service.List(filter));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PaisDto>> Update(string id, PaisRequest request)
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
