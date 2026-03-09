namespace Domain.Entities;

/// <summary>
/// Representa um país com seus dados cadastrais e códigos internacionais.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único no formato ISO 3166-1 alpha-2 (2 caracteres).
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3 (3 caracteres).
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
    /// Código da moeda no formato ISO 4217 (3 caracteres).
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Locale padrão do país (idioma e região).
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;

    /// <summary>
    /// Indicador se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Coleção de estados/províncias do país.
    /// </summary>
    public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();
}
