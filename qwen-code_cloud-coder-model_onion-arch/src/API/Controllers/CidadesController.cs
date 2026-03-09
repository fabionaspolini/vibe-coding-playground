using Application.DTOs;
using Application.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller para gerenciamento de cidades.
/// </summary>
[ApiController]
[Route("cidades")]
public class CidadesController : ControllerBase
{
    private readonly CidadeService _service;

    /// <summary>
    /// Inicializa uma nova instância do <see cref="CidadesController"/>.
    /// </summary>
    public CidadesController(CidadeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CidadeResponse>> Create([FromBody] CreateCidadeRequest request, CancellationToken cancellationToken)
    {
        var cidade = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = cidade.Id }, cidade.ToResponse());
    }

    /// <summary>
    /// Obtém uma cidade pelo ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CidadeResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var cidade = await _service.GetByIdAsync(id, cancellationToken);
        return cidade is null ? NotFound() : Ok(cidade.ToResponse());
    }

    /// <summary>
    /// Lista todas as cidades.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CidadeResponse>>> List(CancellationToken cancellationToken)
    {
        var cidades = await _service.ListAsync(cancellationToken);
        return Ok(cidades.Select(c => c.ToResponse()));
    }

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CidadeResponse>> Update(Guid id, [FromBody] UpdateCidadeRequest request, CancellationToken cancellationToken)
    {
        var cidade = await _service.UpdateAsync(id, request, cancellationToken);
        return Ok(cidade.ToResponse());
    }

    /// <summary>
    /// Remove logicamente uma cidade (marca como inativa).
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.RemoveAsync(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}
