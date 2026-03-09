namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para atualização de um país.
/// </summary>
public record UpdatePaisRequest
{
    /// <summary>
    /// Nome do país.
    /// </summary>
    public string? Nome { get; init; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3 do país.
    /// </summary>
    public string? CodigoISO3 { get; init; }

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int? CodigoONU { get; init; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// </summary>
    public string? CodigoDDI { get; init; }

    /// <summary>
    /// Código da moeda.
    /// </summary>
    public string? CodigoMoeda { get; init; }

    /// <summary>
    /// Locale padrão do país.
    /// </summary>
    public string? DefaultLocale { get; init; }
}

