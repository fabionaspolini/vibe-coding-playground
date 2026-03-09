using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace Geografia.API.Kafka;

/// <summary>
/// Serviço produtor de eventos Kafka.
/// </summary>
public interface IKafkaProducerService
{
    /// <summary>
    /// Produz um evento de criação.
    /// </summary>
    Task ProduceCreateAsync<T>(string topic, string key, T data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Produz um evento de atualização.
    /// </summary>
    Task ProduceUpdateAsync<T>(string topic, string key, T data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Produz um evento de exclusão.
    /// </summary>
    Task ProduceDeleteAsync<T>(string topic, string key, T data, CancellationToken cancellationToken = default);
}

/// <summary>
/// Implementação do serviço produtor de eventos Kafka.
/// </summary>
public class KafkaProducerService : IKafkaProducerService, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly string _bootstrapServers;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="KafkaProducerService"/>.
    /// </summary>
    public KafkaProducerService(string bootstrapServers)
    {
        _bootstrapServers = bootstrapServers;
        var config = new ProducerConfig { BootstrapServers = bootstrapServers };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    /// <summary>
    /// Produz um evento de criação.
    /// </summary>
    public async Task ProduceCreateAsync<T>(string topic, string key, T data, CancellationToken cancellationToken = default)
    {
        var message = new KafkaEvent<T>("CREATE", data);
        await SendMessageAsync(topic, key, message, cancellationToken);
    }

    /// <summary>
    /// Produz um evento de atualização.
    /// </summary>
    public async Task ProduceUpdateAsync<T>(string topic, string key, T data, CancellationToken cancellationToken = default)
    {
        var message = new KafkaEvent<T>("UPDATE", data);
        await SendMessageAsync(topic, key, message, cancellationToken);
    }

    /// <summary>
    /// Produz um evento de exclusão.
    /// </summary>
    public async Task ProduceDeleteAsync<T>(string topic, string key, T data, CancellationToken cancellationToken = default)
    {
        var message = new KafkaEvent<T>("DELETE", data);
        await SendMessageAsync(topic, key, message, cancellationToken);
    }

    private async Task SendMessageAsync<T>(string topic, string key, T message, CancellationToken cancellationToken)
    {
        var messageJson = JsonSerializer.Serialize(message);
        var messageData = new Message<string, string>
        {
            Key = key,
            Value = messageJson
        };

        await _producer.ProduceAsync(topic, messageData, cancellationToken);
    }

    /// <summary>
    /// Libera os recursos do produtor Kafka.
    /// </summary>
    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}

/// <summary>
/// Representa um evento Kafka.
/// </summary>
/// <typeparam name="T">Tipo dos dados do evento.</typeparam>
public class KafkaEvent<T>
{
    /// <summary>
    /// Tipo da ação (CREATE, UPDATE, DELETE).
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Dados do evento.
    /// </summary>
    public T Data { get; set; } = default!;

    /// <summary>
    /// Timestamp do evento.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="KafkaEvent{T}"/>.
    /// </summary>
    public KafkaEvent(string action, T data)
    {
        Action = action;
        Data = data;
    }
}
