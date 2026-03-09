using GeografiaService.Domain.Enums;

namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para resposta de um estado.
/// </summary>
public record EstadoResponse
{
    /// <summary>
    /// Identificador único do estado.
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

    /// <summary>
    /// Indicador se o estado está ativo.
    /// </summary>
    public required bool Ativo { get; init; }
}

