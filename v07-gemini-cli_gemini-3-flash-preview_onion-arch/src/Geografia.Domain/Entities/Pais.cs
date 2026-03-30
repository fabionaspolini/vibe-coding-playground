namespace Geografia.Domain.Entities;

/// <summary>
/// Gerenciar cadastro de paises.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único, sendo no formato do código ISO 3166-1 alpha-2 (Padrão).
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
    /// DDI (Código de discagem).
    /// </summary>
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Idioma principal.
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; } = true;
}
