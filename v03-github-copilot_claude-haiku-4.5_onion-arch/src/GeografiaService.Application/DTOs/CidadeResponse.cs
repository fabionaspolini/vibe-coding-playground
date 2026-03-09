namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para resposta de uma cidade.
/// </summary>
public record CidadeResponse
{
    /// <summary>
    /// Identificador único da cidade.
    /// </summary>
    public required Guid Id { get; init; }

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

    /// <summary>
    /// Indicador se a cidade está ativa.
    /// </summary>
    public required bool Ativo { get; init; }
}

