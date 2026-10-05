using System.Text;
using System.Text.Json;
using Confluent.Kafka;

public class AlertProduce
{
    private readonly IProducer<string, string> _producer;
    public AlertProduce()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost"
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }
    public void Produce(string topic, string text)
    {
        var message = new Message<string, string>
        {
            Value = text
        };
        _producer.Produce(topic, message);
    }
    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}