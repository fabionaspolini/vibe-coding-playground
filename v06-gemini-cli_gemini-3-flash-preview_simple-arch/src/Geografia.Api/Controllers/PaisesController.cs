using Geografia.Api.Data;
using Geografia.Api.Domain.Entities;
using Geografia.Api.Dtos;
using Geografia.Api.Extensions;
using Geografia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Geografia.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de Países.
/// </summary>
[ApiController]
[Route("paises")]
public class PaisesController(GeografiaDbContext context, KafkaProducerService kafka) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PaisResponse>> Create(PaisRequest request)
    {
        var entity = new Pais { Id = request.Id };
        entity.UpdateFromRequest(request);

        context.Paises.Add(entity);
        await context.SaveChangesAsync();

        kafka.Produce("pais", entity.Id, entity.ToResponse(), "create");

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToResponse());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaisResponse>> GetById(string id)
        => await context.Paises.FindAsync(id) is Pais entity ? entity.ToResponse() : NotFound();

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisResponse>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? codigoIso3,
        [FromQuery] int? codigoOnu,
        [FromQuery] string? codigoDdi,
        [FromQuery] string? codigoMoeda,
        [FromQuery] string? defaultLocale,
        [FromQuery] bool? ativo)
    {
        var query = context.Paises.AsQueryable();

        if (nome is not null) query = query.Where(p => p.Nome.Contains(nome));
        if (codigoIso3 is not null) query = query.Where(p => p.CodigoISO3 == codigoIso3);
        if (codigoOnu is not null) query = query.Where(p => p.CodigoONU == codigoOnu);
        if (codigoDdi is not null) query = query.Where(p => p.CodigoDDI == codigoDdi);
        if (codigoMoeda is not null) query = query.Where(p => p.CodigoMoeda == codigoMoeda);
        if (defaultLocale is not null) query = query.Where(p => p.DefaultLocale == defaultLocale);
        if (ativo is not null) query = query.Where(p => p.Ativo == ativo);

        var result = await query.Select(p => p.ToResponse()).ToListAsync();
        return result;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, PaisRequest request)
    {
        var entity = await context.Paises.FindAsync(id);
        if (entity is null) return NotFound();

        entity.UpdateFromRequest(request);
        await context.SaveChangesAsync();

        kafka.Produce("pais", entity.Id, entity.ToResponse(), "update");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        var entity = await context.Paises.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Ativo = false;
        await context.SaveChangesAsync();

        kafka.Produce("pais", entity.Id, entity.ToResponse(), "delete");

        return NoContent();
    }
}
