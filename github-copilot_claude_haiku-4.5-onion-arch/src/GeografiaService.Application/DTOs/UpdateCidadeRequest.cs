namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para atualização de uma cidade.
/// </summary>
public record UpdateCidadeRequest
{
    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string? Nome { get; init; }

    /// <summary>
    /// Código postal da cidade.
    /// </summary>
    public string? CodigoPostal { get; init; }

    /// <summary>
    /// Coordenada de latitude.
    /// </summary>
    public decimal? Latitude { get; init; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public decimal? Longitude { get; init; }
}

