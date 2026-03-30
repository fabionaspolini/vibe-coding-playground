using Geografia.Api.Data;
using Geografia.Api.Domain.Entities;
using Geografia.Api.Dtos;
using Geografia.Api.Extensions;
using Geografia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Geografia.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de Cidades.
/// </summary>
[ApiController]
[Route("cidades")]
public class CidadesController(GeografiaDbContext context, KafkaProducerService kafka) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CidadeResponse>> Create(CidadeRequest request)
    {
        var entity = new Cidade { Id = Guid.NewGuid() }; // UUID v7 if available, otherwise standard Guid
        entity.UpdateFromRequest(request);

        context.Cidades.Add(entity);
        await context.SaveChangesAsync();

        kafka.Produce("cidade", entity.Id.ToString(), entity.ToResponse(), "create");

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToResponse());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CidadeResponse>> GetById(Guid id)
        => await context.Cidades.FindAsync(id) is Cidade entity ? entity.ToResponse() : NotFound();

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CidadeResponse>>> List(
        [FromQuery] string? estadoId,
        [FromQuery] string? nome,
        [FromQuery] string? codigoPostal,
        [FromQuery] bool? ativo)
    {
        var query = context.Cidades.AsQueryable();

        if (estadoId is not null) query = query.Where(c => c.EstadoId == estadoId);
        if (nome is not null) query = query.Where(c => c.Nome.Contains(nome));
        if (codigoPostal is not null) query = query.Where(c => c.CodigoPostal == codigoPostal);
        if (ativo is not null) query = query.Where(c => c.Ativo == ativo);

        var result = await query.Select(c => c.ToResponse()).ToListAsync();
        return result;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CidadeRequest request)
    {
        var entity = await context.Cidades.FindAsync(id);
        if (entity is null) return NotFound();

        entity.UpdateFromRequest(request);
        await context.SaveChangesAsync();

        kafka.Produce("cidade", entity.Id.ToString(), entity.ToResponse(), "update");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var entity = await context.Cidades.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Ativo = false;
        await context.SaveChangesAsync();

        kafka.Produce("cidade", entity.Id.ToString(), entity.ToResponse(), "delete");

        return NoContent();
    }
}
