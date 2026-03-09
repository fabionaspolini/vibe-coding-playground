namespace Geografia.API.Domain;

/// <summary>
/// Representa um país no sistema de geografia.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único do país, no formato ISO 3166-1 alpha-2.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3 do país.
    /// </summary>
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; set; }

    /// <summary>
    /// Código DDI (discagem direta internacional).
    /// </summary>
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda no formato ISO 4217.
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Locale padrão do país (idioma e região).
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Coleção de estados pertencentes ao país.
    /// </summary>
    public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();
}
