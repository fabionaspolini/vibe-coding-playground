namespace Application.DTOs;

/// <summary>
/// DTO para atualização de uma cidade existente.
/// </summary>
public class UpdateCidadeRequest
{
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
}
