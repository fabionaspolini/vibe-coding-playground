namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para criação de uma nova cidade.
/// </summary>
public record CreateCidadeRequest
{
    /// <summary>
    /// Identificador do estado ao qual esta cidade pertence.
    /// </summary>
    public required string EstadoId { get; init; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public required string Nome { get; init; }

    /// <summary>
    /// Código postal da cidade.
    /// </summary>
    public required string CodigoPostal { get; init; }

    /// <summary>
    /// Coordenada de latitude.
    /// </summary>
    public required decimal Latitude { get; init; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public required decimal Longitude { get; init; }
}

