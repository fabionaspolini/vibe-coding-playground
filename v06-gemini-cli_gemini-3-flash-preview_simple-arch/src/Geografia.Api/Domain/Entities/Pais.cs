using System.ComponentModel.DataAnnotations;

namespace Geografia.Api.Domain.Entities;

/// <summary>
/// Representa um País.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único, no formato ISO 3166-1 alpha-2 (ex: "BR").
    /// </summary>
    [Key, StringLength(2)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    [Required]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3 (ex: "BRA").
    /// </summary>
    [Required, StringLength(3)]
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; set; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// </summary>
    [Required]
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    [Required, StringLength(3)]
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Idioma principal do país.
    /// </summary>
    [Required]
    public string DefaultLocale { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o país está ativo no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Lista de estados vinculados ao país.
    /// </summary>
    public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();
}
