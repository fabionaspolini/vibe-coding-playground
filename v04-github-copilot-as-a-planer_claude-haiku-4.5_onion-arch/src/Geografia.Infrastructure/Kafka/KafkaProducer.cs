namespace Geografia.Infrastructure.Kafka;

using Confluent.Kafka;
using System.Text.Json;

/// <summary>
/// Produtor de eventos Kafka para a aplicação de Geografia.
/// </summary>
public interface IKafkaProducer
{
    /// <summary>
    /// Produz uma mensagem para um tópico Kafka.
    /// </summary>
    /// <typeparam name="TKey">Tipo da chave da mensagem.</typeparam>
    /// <typeparam name="TValue">Tipo do valor da mensagem.</typeparam>
    /// <param name="topic">Nome do tópico.</param>
    /// <param name="key">Chave da mensagem.</param>
    /// <param name="value">Valor da mensagem.</param>
    void Produce<TKey, TValue>(string topic, TKey key, TValue value);
}

/// <summary>
/// Implementação do produtor Kafka.
/// </summary>
public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    /// <summary>
    /// Construtor do produtor Kafka.
    /// </summary>
    /// <param name="bootstrapServers">Servidores bootstrap do Kafka.</param>
    public KafkaProducer(string bootstrapServers)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            Acks = Acks.All,
            RetryBackoffMs = 100,
            MessageTimeoutMs = 5000
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    /// <summary>
    /// Produz uma mensagem para um tópico Kafka.
    /// </summary>
    /// <typeparam name="TKey">Tipo da chave da mensagem.</typeparam>
    /// <typeparam name="TValue">Tipo do valor da mensagem.</typeparam>
    /// <param name="topic">Nome do tópico.</param>
    /// <param name="key">Chave da mensagem.</param>
    /// <param name="value">Valor da mensagem.</param>
    public void Produce<TKey, TValue>(string topic, TKey key, TValue value)
    {
        var stringKey = key?.ToString() ?? string.Empty;
        var stringValue = JsonSerializer.Serialize(value);

        _producer.Produce(topic, new Message<string, string>
        {
            Key = stringKey,
            Value = stringValue
        }, DeliveryReport);
    }

    /// <summary>
    /// Callback para relatório de entrega da mensagem.
    /// </summary>
    /// <param name="deliveryReport">Relatório de entrega.</param>
    private static void DeliveryReport(DeliveryReport<string, string> deliveryReport)
    {
        if (deliveryReport.Error.Code != ErrorCode.NoError)
        {
            Console.WriteLine($"Falha ao entregar mensagem ao tópico {deliveryReport.Topic}: {deliveryReport.Error.Reason}");
        }
        else
        {
            Console.WriteLine($"Mensagem entregue ao tópico {deliveryReport.Topic} (partição {deliveryReport.Partition.Value}, offset {deliveryReport.Offset.Value})");
        }
    }
}

