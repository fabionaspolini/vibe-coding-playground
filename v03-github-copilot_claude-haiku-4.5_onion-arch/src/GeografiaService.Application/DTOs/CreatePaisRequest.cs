namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para criação de um novo país.
/// </summary>
public record CreatePaisRequest
{
    /// <summary>
    /// Identificador único do país no formato ISO 3166-1 alpha-2.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public required string Nome { get; init; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3 do país.
    /// </summary>
    public required string CodigoISO3 { get; init; }

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public required int CodigoONU { get; init; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// </summary>
    public required string CodigoDDI { get; init; }

    /// <summary>
    /// Código da moeda.
    /// </summary>
    public required string CodigoMoeda { get; init; }

    /// <summary>
    /// Locale padrão do país.
    /// </summary>
    public required string DefaultLocale { get; init; }
}

