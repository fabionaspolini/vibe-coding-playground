namespace Geografia.Application.DTOs;

/// <summary>
/// DTO para criar um novo estado.
/// </summary>
public class CriarEstadoDto
{
    /// <summary>
    /// Identificador único, sendo no formato do código ISO 3166-2.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Identificador do país ao qual o estado pertence.
    /// </summary>
    public required string PaisId { get; set; }

    /// <summary>
    /// Nome do estado/província/departamento.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão no país.
    /// </summary>
    public required string Tipo { get; set; }
}

/// <summary>
/// DTO para atualizar um estado existente.
/// </summary>
public class AtualizarEstadoDto
{
    /// <summary>
    /// Nome do estado/província/departamento.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão no país.
    /// </summary>
    public required string Tipo { get; set; }
}

/// <summary>
/// DTO para resposta de estado.
/// </summary>
public class EstadoDto
{
    /// <summary>
    /// Identificador único, sendo no formato do código ISO 3166-2.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Identificador do país ao qual o estado pertence.
    /// </summary>
    public required string PaisId { get; set; }

    /// <summary>
    /// Nome do estado/província/departamento.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão no país.
    /// </summary>
    public required string Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public required bool Ativo { get; set; }
}

