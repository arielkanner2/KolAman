using System.Threading.Tasks;

namespace MyNamespace
{
    class MyClassCS
    {
        static async Task Main()
        {
            RabbitConsumer rabbitConsumer = new RabbitConsumer();
            await rabbitConsumer.ConsumeQueue();
        }
    }
}