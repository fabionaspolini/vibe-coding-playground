using Geografia.Api.Domain.Enums;

namespace Geografia.Api.Dtos;

/// <summary>
/// Representa a requisição para criação/atualização de um Estado.
/// </summary>
public record EstadoRequest
{
    /// <summary>
    /// Identificador único, no formato ISO 3166-2 (ex: "BR-SP").
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Identificador do País.
    /// </summary>
    public string PaisId { get; init; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado.
    /// </summary>
    public string Sigla { get; init; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão administrativa.
    /// </summary>
    public SubdivisaoTipo Tipo { get; init; }
}

/// <summary>
/// Representa a resposta de dados de um Estado.
/// </summary>
public record EstadoResponse
{
    /// <summary>
    /// Identificador único do estado.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Identificador do País.
    /// </summary>
    public string PaisId { get; init; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado.
    /// </summary>
    public string Sigla { get; init; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão administrativa.
    /// </summary>
    public SubdivisaoTipo Tipo { get; init; }

    /// <summary>
    /// Indica se o estado está ativo.
    /// </summary>
    public bool Ativo { get; init; }
}
