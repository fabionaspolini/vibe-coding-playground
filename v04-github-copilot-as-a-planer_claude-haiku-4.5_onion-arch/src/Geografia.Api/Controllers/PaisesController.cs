using Geografia.Domain.Entities;

namespace Geografia.Api.Controllers;

using Geografia.Application.DTOs;
using Geografia.Application.Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller para gerenciar países.
/// </summary>
[ApiController]
[Route("paises")]
// [Authorize] // TODO: Ativar quando implementar autenticação JWT
public class PaisesController : ControllerBase
{
    private readonly IPaisService _service;

    /// <summary>
    /// Construtor da controller de países.
    /// </summary>
    /// <param name="service">Serviço de países.</param>
    public PaisesController(IPaisService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    /// <param name="dto">Dados do país a ser criado.</param>
    /// <returns>Código 201 e os dados do país criado.</returns>
    [HttpPost]
    public async Task<ActionResult<PaisDto>> Create([FromBody] CriarPaisDto dto)
    {
        var pais = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = pais.Id }, pais);
    }

    /// <summary>
    /// Obtém um país pelo ID.
    /// </summary>
    /// <param name="id">ID do país (código ISO 3166-1 alpha-2).</param>
    /// <returns>Dados do país ou 404 se não encontrado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PaisDto>> GetById(string id)
    {
        var pais = await _service.GetByIdAsync(id);
        if (pais == null)
            return NotFound();

        return Ok(pais);
    }

    /// <summary>
    /// Lista países com filtros opcionais.
    /// </summary>
    /// <param name="filters">Parâmetros de query para filtrar por atributos.</param>
    /// <returns>Lista de países.</returns>
    [HttpGet]
    public async Task<ActionResult<List<PaisDto>>> List([FromQuery] Dictionary<string, string> filters)
    {
        var filterDict = filters?.ToDictionary(x => x.Key, x => (object)x.Value);
        var paises = await _service.ListAsync(filterDict);
        return Ok(paises);
    }

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    /// <param name="id">ID do país a ser atualizado.</param>
    /// <param name="dto">Dados atualizados do país.</param>
    /// <returns>Dados do país atualizado.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<PaisDto>> Update(string id, [FromBody] AtualizarPaisDto dto)
    {
        var pais = await _service.UpdateAsync(id, dto);
        return Ok(pais);
    }

    /// <summary>
    /// Remove (desativa) um país.
    /// </summary>
    /// <param name="id">ID do país a ser removido.</param>
    /// <returns>Código 204 No Content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        await _service.RemoveAsync(id);
        return NoContent();
    }
}

