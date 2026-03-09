using Confluent.Kafka;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Kafka;

/// <summary>
/// Serviço para publicação de eventos no Kafka.
/// </summary>
public class KafkaEventService : IKafkaEventService
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaEventService> _logger;
    private readonly string _bootstrapServers;

    /// <summary>
    /// Inicializa uma nova instância do <see cref="KafkaEventService"/>.
    /// </summary>
    public KafkaEventService(ILogger<KafkaEventService> logger, string bootstrapServers = "localhost:9092")
    {
        _logger = logger;
        _bootstrapServers = bootstrapServers;

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            ClientId = "geografia-api"
        };

        _producer = new ProducerBuilder<string, string>(config)
            .SetErrorHandler((_, error) =>
                _logger.LogError("Erro no produtor Kafka: {Reason}", error.Reason))
            .Build();
    }

    /// <inheritdoc />
    public void PublishCreate<T>(string topic, string key, T data) =>
        PublishEvent(topic, key, data, "CREATE");

    /// <inheritdoc />
    public void PublishUpdate<T>(string topic, string key, T data) =>
        PublishEvent(topic, key, data, "UPDATE");

    /// <inheritdoc />
    public void PublishDelete(string topic, string key) =>
        PublishEvent(topic, key, new { Action = "DELETE", Id = key }, "DELETE");

    private void PublishEvent<T>(string topic, string key, T data, string action)
    {
        var message = new
        {
            Action = action,
            Timestamp = DateTime.UtcNow,
            Data = data
        };

        var messageJson = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = key,
            Value = messageJson
        };

        try
        {
            _producer.Produce(topic, kafkaMessage, deliveryReport =>
            {
                if (deliveryReport.Error.IsError)
                {
                    _logger.LogError(
                        "Falha ao publicar evento no tópico {Topic} com key {Key}: {Error}",
                        topic, key, deliveryReport.Error.Reason);
                }
                else
                {
                    _logger.LogDebug(
                        "Evento publicado com sucesso no tópico {Topic} com key {Key} em {TopicPartitionOffset}",
                        topic, key, deliveryReport.TopicPartitionOffset);
                }
            });
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, "Exceção ao publicar evento no tópico {Topic}", topic);
        }
    }
}
