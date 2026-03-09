using Application.DTOs;
using Application.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller para gerenciamento de países.
/// </summary>
[ApiController]
[Route("paises")]
public class PaisesController : ControllerBase
{
    private readonly PaisService _service;

    /// <summary>
    /// Inicializa uma nova instância do <see cref="PaisesController"/>.
    /// </summary>
    public PaisesController(PaisService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PaisResponse>> Create([FromBody] CreatePaisRequest request, CancellationToken cancellationToken)
    {
        var pais = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = pais.Id }, pais.ToResponse());
    }

    /// <summary>
    /// Obtém um país pelo ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PaisResponse>> GetById(string id, CancellationToken cancellationToken)
    {
        var pais = await _service.GetByIdAsync(id, cancellationToken);
        return pais is null ? NotFound() : Ok(pais.ToResponse());
    }

    /// <summary>
    /// Lista todos os países.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisResponse>>> List(CancellationToken cancellationToken)
    {
        var paises = await _service.ListAsync(cancellationToken);
        return Ok(paises.Select(p => p.ToResponse()));
    }

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PaisResponse>> Update(string id, [FromBody] UpdatePaisRequest request, CancellationToken cancellationToken)
    {
        var pais = await _service.UpdateAsync(id, request, cancellationToken);
        return Ok(pais.ToResponse());
    }

    /// <summary>
    /// Remove logicamente um país (marca como inativo).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        var result = await _service.RemoveAsync(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}
