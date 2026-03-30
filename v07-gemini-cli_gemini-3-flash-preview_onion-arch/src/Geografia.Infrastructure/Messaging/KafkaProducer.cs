using Confluent.Kafka;
using Geografia.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Geografia.Infrastructure.Messaging;

public class KafkaProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    public KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092"
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public void Produce<T>(string topic, string key, T message)
    {
        var val = JsonSerializer.Serialize(message);
        _producer.Produce(topic, new Message<string, string> { Key = key, Value = val }, DeliveryReportHandler);
    }

    private void DeliveryReportHandler(DeliveryReport<string, string> report)
    {
        if (report.Error.IsError)
        {
            _logger.LogError("Falha ao enviar mensagem para Kafka: {Error}", report.Error.Reason);
        }
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}
