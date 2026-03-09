namespace Geografia.API.Domain;

/// <summary>
/// Representa uma cidade no sistema de geografia.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único da cidade (UUID v7).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identificador do estado ao qual a cidade pertence.
    /// </summary>
    public string EstadoId { get; set; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código postal da cidade.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Latitude da cidade.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude da cidade.
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Indica se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Estado ao qual a cidade pertence.
    /// </summary>
    public virtual Estado? Estado { get; set; }
}
