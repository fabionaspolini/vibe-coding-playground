namespace Domain.Interfaces;

/// <summary>
/// Interface para serviço de publicação de eventos Kafka.
/// </summary>
public interface IKafkaEventService
{
    /// <summary>
    /// Publica um evento de criação.
    /// </summary>
    void PublishCreate<T>(string topic, string key, T data);

    /// <summary>
    /// Publica um evento de atualização.
    /// </summary>
    void PublishUpdate<T>(string topic, string key, T data);

    /// <summary>
    /// Publica um evento de exclusão.
    /// </summary>
    void PublishDelete(string topic, string key);
}
