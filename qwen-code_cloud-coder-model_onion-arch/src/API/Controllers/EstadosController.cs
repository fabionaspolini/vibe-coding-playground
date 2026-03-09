using Application.DTOs;
using Application.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller para gerenciamento de estados.
/// </summary>
[ApiController]
[Route("estados")]
public class EstadosController : ControllerBase
{
    private readonly EstadoService _service;

    /// <summary>
    /// Inicializa uma nova instância do <see cref="EstadosController"/>.
    /// </summary>
    public EstadosController(EstadoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EstadoResponse>> Create([FromBody] CreateEstadoRequest request, CancellationToken cancellationToken)
    {
        var estado = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = estado.Id }, estado.ToResponse());
    }

    /// <summary>
    /// Obtém um estado pelo ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoResponse>> GetById(string id, CancellationToken cancellationToken)
    {
        var estado = await _service.GetByIdAsync(id, cancellationToken);
        return estado is null ? NotFound() : Ok(estado.ToResponse());
    }

    /// <summary>
    /// Lista todos os estados.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoResponse>>> List(CancellationToken cancellationToken)
    {
        var estados = await _service.ListAsync(cancellationToken);
        return Ok(estados.Select(e => e.ToResponse()));
    }

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<EstadoResponse>> Update(string id, [FromBody] UpdateEstadoRequest request, CancellationToken cancellationToken)
    {
        var estado = await _service.UpdateAsync(id, request, cancellationToken);
        return Ok(estado.ToResponse());
    }

    /// <summary>
    /// Remove logicamente um estado (marca como inativo).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        var result = await _service.RemoveAsync(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}
