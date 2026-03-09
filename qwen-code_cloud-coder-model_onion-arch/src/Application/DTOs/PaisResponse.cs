namespace Application.DTOs;

/// <summary>
/// DTO para resposta de dados de um país.
/// </summary>
public class PaisResponse
{
    /// <summary>
    /// Identificador único no formato ISO 3166-1 alpha-2.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; set; }

    /// <summary>
    /// Código DDI (Discagem Direta Internacional).
    /// </summary>
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda no formato ISO 4217.
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Locale padrão do país.
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;

    /// <summary>
    /// Indicador se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; }
}
