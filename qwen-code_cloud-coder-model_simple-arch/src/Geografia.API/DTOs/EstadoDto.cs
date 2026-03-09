namespace Geografia.API.DTOs;

/// <summary>
/// DTO para criação de um estado.
/// </summary>
public class EstadoCreateDto
{
    /// <summary>
    /// Identificador único do estado (ISO 3166-2).
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Identificador do país.
    /// </summary>
    public string PaisId { get; set; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;
}

/// <summary>
/// DTO para atualização de um estado.
/// </summary>
public class EstadoUpdateDto
{
    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;
}

/// <summary>
/// DTO para resposta de um estado.
/// </summary>
public class EstadoResponseDto
{
    /// <summary>
    /// Identificador único do estado.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Identificador do país.
    /// </summary>
    public string PaisId { get; set; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; }
}
