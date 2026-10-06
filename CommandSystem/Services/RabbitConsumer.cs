// using System.Text;
// using System.Threading.Tasks;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;


// public class RabbitConsumer
// {
//   private IConnection _connection;
//   private IChannel _channel;
//   private ManualResetEvent _resetEvent = new ManualResetEvent(false);

//   public async Task ConsumeQueue()
//   {
//     var factory = new ConnectionFactory
//     {
//         Uri = new Uri("amqp://localhost")
//     };

//     _connection = await factory.CreateConnectionAsync();
//     _channel = await _connection.CreateChannelAsync();

//     // ensure that the queue exists before we access it
//     var queueName = "alerts2";
//     bool durable = false;
//     bool exclusive = false;
//     bool autoDelete = false;

//     await _channel.QueueDeclareAsync(queueName,)(queueName, durable, exclusive, autoDelete, null);

//     var consumer = new AsyncEventingBasicConsumer(_channel);

//     // add the message receive event
//     consumer.ReceivedAsync += async (model, deliveryEventArgs) =>
//     {
//         var body = deliveryEventArgs.Body.ToArray();
//         // convert the message back from byte[] to a string
//         var message = Encoding.UTF8.GetString(body);
//         Console.WriteLine("** Received message: {0} by Consumer thread **", message);
//         // ack the message, ie. confirm that we have processed it
//         // otherwise it will be requeued a bit later
//         await _channel.BasicAckAsync(deliveryEventArgs.DeliveryTag, false);
//     };

//     // start consuming
//     _ = await _channel.BasicConsumeAsync(queueName, false, consumer);
//     // Wait for the reset event and clean up when it triggers
//     _resetEvent.WaitOne();
//     _channel?.CloseAsync();
//     _channel = null;
//     _connection?.CloseAsync();
//     _connection = null;
//   }
// }
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;

public class RabbitConsumer
{
    public async Task<List<Alert>> ConsumeQueue(string queueName)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        var consumeList = new List<string>();
        List<Alert> alertsList = new List<Alert>();

        await channel.ExchangeDeclareAsync(exchange: "logs",
        type: ExchangeType.Fanout);

        // declare a server-named queue
        // QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
        // string queueName = queueDeclareResult.QueueName;
        // await channel.QueueBindAsync(queue: queueName, exchange: "logs", routingKey: string.Empty);
        await channel.QueueBindAsync(queue: queueName, exchange: "logs", routingKey: "");

        Console.WriteLine(" [*] Waiting for logs.");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            // Console.WriteLine($" [x] {message}");
            consumeList.Add(message);
            System.Console.WriteLine(consumeList.Count());
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
        
        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
        System.Console.WriteLine(consumeList.Count());
        foreach (string rabbitMessage in consumeList)
        {
            alertsList.Add(JsonSerializer.Deserialize<Alert>(rabbitMessage));
        }

        return alertsList;
    }
}
