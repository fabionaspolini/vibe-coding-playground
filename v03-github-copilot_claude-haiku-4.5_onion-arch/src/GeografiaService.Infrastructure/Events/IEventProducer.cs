namespace GeografiaService.Infrastructure.Events;

/// <summary>
/// Interface para produção de eventos no Kafka.
/// </summary>
public interface IEventProducer
{
    /// <summary>
    /// Produz um evento no Kafka para um tópico específico.
    /// </summary>
    /// <param name="topico">Nome do tópico Kafka.</param>
    /// <param name="chave">Chave da mensagem (normalmente o ID da entidade).</param>
    /// <param name="valor">Valor da mensagem em formato JSON.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    Task ProduceEventAsync(string topico, string chave, string valor, CancellationToken cancellationToken = default);
}

