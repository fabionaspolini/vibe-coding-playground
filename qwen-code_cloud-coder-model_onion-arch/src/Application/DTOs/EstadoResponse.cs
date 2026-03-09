using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// DTO para resposta de dados de um estado.
/// </summary>
public class EstadoResponse
{
    /// <summary>
    /// Identificador único no formato ISO 3166-2.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Identificador do país ao qual o estado pertence.
    /// </summary>
    public string PaisId { get; set; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado (sem o prefixo do país).
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão.
    /// </summary>
    public SubdivisionType Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; }
}
