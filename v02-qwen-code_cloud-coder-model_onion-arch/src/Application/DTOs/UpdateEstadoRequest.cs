namespace Application.DTOs;

/// <summary>
/// DTO para atualização de um estado existente.
/// </summary>
public class UpdateEstadoRequest
{
    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado (sem o prefixo do país).
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;
}
