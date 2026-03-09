using GeografiaService.Domain.Enums;

namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para criação de um novo estado.
/// </summary>
public record CreateEstadoRequest
{
    /// <summary>
    /// Identificador único do estado no formato ISO 3166-2.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Identificador do país ao qual este estado pertence.
    /// </summary>
    public required string PaisId { get; init; }

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public required string Nome { get; init; }

    /// <summary>
    /// Sigla nacional do estado.
    /// </summary>
    public required string Sigla { get; init; }

    /// <summary>
    /// Tipo da subdivisão dentro do país.
    /// </summary>
    public required EstadoTipo Tipo { get; init; }
}

