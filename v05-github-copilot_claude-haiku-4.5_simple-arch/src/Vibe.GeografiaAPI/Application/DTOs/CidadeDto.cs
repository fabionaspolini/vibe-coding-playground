namespace Vibe.GeografiaAPI.Application.DTOs;

/// <summary>
/// DTO para requisições de criação de Cidade.
/// </summary>
public class CreateCidadeDto
{
    /// <summary>
    /// Código ISO 3166-2 do estado ao qual a cidade pertence.
    /// </summary>
    public required string EstadoId { get; set; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código postal da cidade.
    /// </summary>
    public required string CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude (-90.0 a 90.0).
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude (-180.0 a 180.0).
    /// </summary>
    public required decimal Longitude { get; set; }
}

/// <summary>
/// DTO para requisições de atualização de Cidade.
/// </summary>
public class UpdateCidadeDto
{
    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string? Nome { get; set; }

    /// <summary>
    /// Código postal da cidade.
    /// </summary>
    public string? CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude.
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public decimal? Longitude { get; set; }
}

/// <summary>
/// DTO para resposta de leitura de Cidade.
/// </summary>
public class CidadeDto
{
    /// <summary>
    /// Identificador único (UUID v7).
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Código ISO 3166-2 do estado.
    /// </summary>
    public required string EstadoId { get; set; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código postal da cidade.
    /// </summary>
    public required string CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude.
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public required decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; }
}

