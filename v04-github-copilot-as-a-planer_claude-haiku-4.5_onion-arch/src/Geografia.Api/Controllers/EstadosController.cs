namespace Geografia.Api.Controllers;

using Geografia.Application.DTOs;
using Geografia.Application.Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller para gerenciar estados.
/// </summary>
[ApiController]
[Route("estados")]
// [Authorize] // TODO: Ativar quando implementar autenticação JWT
public class EstadosController : ControllerBase
{
    private readonly IEstadoService _service;

    /// <summary>
    /// Construtor da controller de estados.
    /// </summary>
    /// <param name="service">Serviço de estados.</param>
    public EstadosController(IEstadoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    /// <param name="dto">Dados do estado a ser criado.</param>
    /// <returns>Código 201 e os dados do estado criado.</returns>
    [HttpPost]
    public async Task<ActionResult<EstadoDto>> Create([FromBody] CriarEstadoDto dto)
    {
        var estado = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = estado.Id }, estado);
    }

    /// <summary>
    /// Obtém um estado pelo ID.
    /// </summary>
    /// <param name="id">ID do estado (código ISO 3166-2).</param>
    /// <returns>Dados do estado ou 404 se não encontrado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoDto>> GetById(string id)
    {
        var estado = await _service.GetByIdAsync(id);
        if (estado == null)
            return NotFound();

        return Ok(estado);
    }

    /// <summary>
    /// Lista estados com filtros opcionais.
    /// </summary>
    /// <param name="filters">Parâmetros de query para filtrar por atributos.</param>
    /// <returns>Lista de estados.</returns>
    [HttpGet]
    public async Task<ActionResult<List<EstadoDto>>> List([FromQuery] Dictionary<string, string> filters)
    {
        var filterDict = filters?.ToDictionary(x => x.Key, x => (object)x.Value);
        var estados = await _service.ListAsync(filterDict);
        return Ok(estados);
    }

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    /// <param name="id">ID do estado a ser atualizado.</param>
    /// <param name="dto">Dados atualizados do estado.</param>
    /// <returns>Dados do estado atualizado.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EstadoDto>> Update(string id, [FromBody] AtualizarEstadoDto dto)
    {
        var estado = await _service.UpdateAsync(id, dto);
        return Ok(estado);
    }

    /// <summary>
    /// Remove (desativa) um estado.
    /// </summary>
    /// <param name="id">ID do estado a ser removido.</param>
    /// <returns>Código 204 No Content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        await _service.RemoveAsync(id);
        return NoContent();
    }
}

