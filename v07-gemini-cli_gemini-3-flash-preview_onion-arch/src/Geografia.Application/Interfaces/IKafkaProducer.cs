namespace Geografia.Application.Interfaces;

public interface IKafkaProducer
{
    void Produce<T>(string topic, string key, T message);
}
