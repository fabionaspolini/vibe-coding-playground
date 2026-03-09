namespace Application.DTOs;

/// <summary>
/// DTO para resposta de dados de uma cidade.
/// </summary>
public class CidadeResponse
{
    /// <summary>
    /// Identificador único (UUID).
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
    /// Código postal (CEP/Zip code).
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
    /// Indicador se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; }
}
