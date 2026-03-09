using Confluent.Kafka;

namespace GeografiaService.Infrastructure.Events;

/// <summary>
/// Implementação do produtor de eventos no Kafka.
/// </summary>
public class KafkaEventProducer : IEventProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaEventProducer> _logger;

    /// <summary>
    /// Inicializa uma nova instância do produtor de eventos Kafka.
    /// </summary>
    public KafkaEventProducer(IProducer<string, string> producer, ILogger<KafkaEventProducer> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    /// <summary>
    /// Produz um evento no Kafka para um tópico específico.
    /// </summary>
    public Task ProduceEventAsync(string topico, string chave, string valor, CancellationToken cancellationToken = default)
    {
        var message = new Message<string, string> { Key = chave, Value = valor };

        _producer.Produce(topico, message, (deliveryReport) =>
        {
            if (deliveryReport.Error.Code != ErrorCode.NoError)
            {
                _logger.LogError($"Erro ao produzir mensagem no tópico '{topico}': {deliveryReport.Error.Reason}");
            }
            else
            {
                _logger.LogInformation($"Mensagem produzida com sucesso no tópico '{topico}' para partição {deliveryReport.Partition}");
            }
        });

        return Task.CompletedTask;
    }
}

