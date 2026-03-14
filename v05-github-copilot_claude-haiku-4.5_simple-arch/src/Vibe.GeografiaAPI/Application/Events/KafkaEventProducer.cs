using Confluent.Kafka;
using System.Text.Json;

namespace Vibe.GeografiaAPI.Application.Events;

/// <summary>
/// Produtor de eventos Kafka para publicação de eventos de CRUD das entidades geográficas.
/// </summary>
public class KafkaEventProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaEventProducer> _logger;

    /// <summary>
    /// Inicializa uma nova instância do produtor Kafka.
    /// </summary>
    public KafkaEventProducer(IConfiguration configuration, ILogger<KafkaEventProducer> logger)
    {
        _logger = logger;
        var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
        
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    /// <summary>
    /// Produz um evento no tópico Kafka especificado.
    /// </summary>
    /// <param name="topic">Nome do tópico Kafka (ex: "geografia.pais").</param>
    /// <param name="entityId">ID da entidade como chave da mensagem.</param>
    /// <param name="eventData">Dados do evento em forma de objeto anônimo.</param>
    public void ProduceEvent(string topic, string entityId, object eventData)
    {
        try
        {
            var message = new Message<string, string>
            {
                Key = entityId,
                Value = JsonSerializer.Serialize(eventData)
            };

            _producer.Produce(topic, message, DeliveryReport);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao produzir evento para o tópico {Topic} com ID {EntityId}", topic, entityId);
            throw;
        }
    }

    /// <summary>
    /// Callback para tratamento de respostas de entrega de mensagens Kafka.
    /// </summary>
    private void DeliveryReport(DeliveryReport<string, string> report)
    {
        if (report.Error.Code != ErrorCode.NoError)
        {
            _logger.LogError(
                "Falha na entrega da mensagem ao tópico {Topic}: {Reason}",
                report.Topic,
                report.Error.Reason
            );
        }
        else
        {
            _logger.LogInformation(
                "Mensagem entregue ao tópico {Topic}, partição {Partition}, offset {Offset}",
                report.Topic,
                report.Partition,
                report.Offset
            );
        }
    }

    /// <summary>
    /// Libera os recursos do produtor Kafka.
    /// </summary>
    public void Dispose() => _producer?.Dispose();
}

