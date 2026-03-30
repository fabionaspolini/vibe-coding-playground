using Geografia.Api.Data;
using Geografia.Api.Domain.Entities;
using Geografia.Api.Domain.Enums;
using Geografia.Api.Dtos;
using Geografia.Api.Extensions;
using Geografia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Geografia.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de Estados.
/// </summary>
[ApiController]
[Route("estados")]
public class EstadosController(GeografiaDbContext context, KafkaProducerService kafka) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EstadoResponse>> Create(EstadoRequest request)
    {
        var entity = new Estado { Id = request.Id };
        entity.UpdateFromRequest(request);

        context.Estados.Add(entity);
        await context.SaveChangesAsync();

        kafka.Produce("estado", entity.Id, entity.ToResponse(), "create");

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToResponse());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoResponse>> GetById(string id)
        => await context.Estados.FindAsync(id) is Estado entity ? entity.ToResponse() : NotFound();

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoResponse>>> List(
        [FromQuery] string? paisId,
        [FromQuery] string? nome,
        [FromQuery] string? sigla,
        [FromQuery] SubdivisaoTipo? tipo,
        [FromQuery] bool? ativo)
    {
        var query = context.Estados.AsQueryable();

        if (paisId is not null) query = query.Where(e => e.PaisId == paisId);
        if (nome is not null) query = query.Where(e => e.Nome.Contains(nome));
        if (sigla is not null) query = query.Where(e => e.Sigla == sigla);
        if (tipo is not null) query = query.Where(e => e.Tipo == tipo);
        if (ativo is not null) query = query.Where(e => e.Ativo == ativo);

        var result = await query.Select(e => e.ToResponse()).ToListAsync();
        return result;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, EstadoRequest request)
    {
        var entity = await context.Estados.FindAsync(id);
        if (entity is null) return NotFound();

        entity.UpdateFromRequest(request);
        await context.SaveChangesAsync();

        kafka.Produce("estado", entity.Id, entity.ToResponse(), "update");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        var entity = await context.Estados.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Ativo = false;
        await context.SaveChangesAsync();

        kafka.Produce("estado", entity.Id, entity.ToResponse(), "delete");

        return NoContent();
    }
}
