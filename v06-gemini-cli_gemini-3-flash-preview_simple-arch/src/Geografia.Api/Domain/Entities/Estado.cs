using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Geografia.Api.Domain.Enums;

namespace Geografia.Api.Domain.Entities;

/// <summary>
/// Representa um Estado ou Província de um País.
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único, no formato ISO 3166-2 (ex: "BR-SP").
    /// </summary>
    [Key, StringLength(6)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Referência ao País.
    /// </summary>
    [Required, StringLength(2)]
    public string PaisId { get; set; } = string.Empty;

    /// <summary>
    /// Referência virtual ao País.
    /// </summary>
    [ForeignKey(nameof(PaisId))]
    public virtual Pais? Pais { get; set; }

    /// <summary>
    /// Nome do estado.
    /// </summary>
    [Required]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado (sem o prefixo do país).
    /// </summary>
    [Required]
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão administrativa.
    /// </summary>
    [Required]
    public SubdivisaoTipo Tipo { get; set; }

    /// <summary>
    /// Indica se o estado está ativo no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Lista de cidades vinculadas ao estado.
    /// </summary>
    public virtual ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
}
