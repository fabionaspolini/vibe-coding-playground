namespace Domain.Entities;

/// <summary>
/// Representa uma cidade com suas coordenadas geográficas e dados cadastrais.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único (UUID).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identificador do estado ao qual a cidade pertence (ISO 3166-2).
    /// </summary>
    public string EstadoId { get; set; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código postal (CEP/Zip code) da cidade.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Latitude da cidade (coordenada geográfica).
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude da cidade (coordenada geográfica).
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Estado ao qual a cidade pertence.
    /// </summary>
    public virtual Estado? Estado { get; set; }
}
