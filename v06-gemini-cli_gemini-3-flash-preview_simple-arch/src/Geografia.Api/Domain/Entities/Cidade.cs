using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geografia.Api.Domain.Entities;

/// <summary>
/// Representa uma Cidade.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único (UUID v7).
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Referência ao Estado.
    /// </summary>
    [Required, StringLength(6)]
    public string EstadoId { get; set; } = string.Empty;

    /// <summary>
    /// Referência virtual ao Estado.
    /// </summary>
    [ForeignKey(nameof(EstadoId))]
    public virtual Estado? Estado { get; set; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    [Required]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// CEP ou Zip code local.
    /// </summary>
    [Required]
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Latitude geográfica.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude geográfica.
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Indica se a cidade está ativa no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;
}
