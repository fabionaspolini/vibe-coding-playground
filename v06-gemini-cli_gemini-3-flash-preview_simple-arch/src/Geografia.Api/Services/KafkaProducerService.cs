using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Geografia.Api.Services;

/// <summary>
/// Serviço responsável por produzir eventos no Kafka.
/// </summary>
public class KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger) : IDisposable
{
    private readonly IProducer<string, string> _producer = new ProducerBuilder<string, string>(
        new ProducerConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] }).Build();

    /// <summary>
    /// Produz um evento no Kafka para uma entidade.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade.</typeparam>
    /// <param name="entityName">Nome da entidade para compor o tópico.</param>
    /// <param name="key">Chave da mensagem (Id da entidade).</param>
    /// <param name="data">Dados da mensagem.</param>
    /// <param name="action">Ação realizada (create, update, delete).</param>
    public virtual void Produce<T>(string entityName, string key, T data, string action)
    {
        var topic = $"geografia.{entityName.ToLower()}";
        var payload = JsonSerializer.Serialize(new { Action = action, Data = data });

        var message = new Message<string, string> { Key = key, Value = payload };

        _producer.Produce(topic, message, (report) =>
        {
            if (report.Error.IsError)
            {
                logger.LogError("Falha ao enviar mensagem para o tópico {Topic}: {Error}", topic, report.Error.Reason);
            }
        });
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
        GC.SuppressFinalize(this);
    }
}
