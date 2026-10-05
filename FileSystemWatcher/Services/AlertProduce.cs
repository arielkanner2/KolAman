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
    
}