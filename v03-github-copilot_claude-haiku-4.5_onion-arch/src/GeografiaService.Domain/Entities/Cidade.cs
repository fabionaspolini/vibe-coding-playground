namespace GeografiaService.Domain.Entities;

/// <summary>
/// Entidade que representa uma cidade dentro de um estado.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único da cidade no formato UUID v7.
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();

    /// <summary>
    /// Identificador do estado ao qual esta cidade pertence.
    /// Referência para a entidade Estado.
    /// </summary>
    public required string EstadoId { get; set; }

    /// <summary>
    /// Referência de navegação para o estado.
    /// </summary>
    public Estado? Estado { get; set; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código postal (CEP no Brasil, Zip nos EUA, etc.) da cidade.
    /// </summary>
    public required string CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude para mapas e logística.
    /// Exemplo: -23.5505 para São Paulo.
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude para mapas e logística.
    /// Exemplo: -46.6333 para São Paulo.
    /// </summary>
    public required decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se a cidade está ativa no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;
}

