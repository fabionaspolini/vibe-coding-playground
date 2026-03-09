namespace Geografia.API.DTOs;

/// <summary>
/// DTO para criação de uma cidade.
/// </summary>
public class CidadeCreateDto
{
    /// <summary>
    /// Identificador do estado.
    /// </summary>
    public string EstadoId { get; set; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código postal.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Latitude.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude.
    /// </summary>
    public decimal Longitude { get; set; }
}

/// <summary>
/// DTO para atualização de uma cidade.
/// </summary>
public class CidadeUpdateDto
{
    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código postal.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Latitude.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude.
    /// </summary>
    public decimal Longitude { get; set; }
}

/// <summary>
/// DTO para resposta de uma cidade.
/// </summary>
public class CidadeResponseDto
{
    /// <summary>
    /// Identificador único da cidade.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identificador do estado.
    /// </summary>
    public string EstadoId { get; set; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código postal.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Latitude.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude.
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Indica se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; }
}
