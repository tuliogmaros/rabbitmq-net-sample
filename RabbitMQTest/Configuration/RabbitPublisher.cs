using RabbitMQ.Client;
using System.Text;


namespace RabbitMQTest.Configuration
{
    public class RabbitPublisher
    {
        public string Queue { get; set; }

        public string HostName { get; set; }

        public string Message { get; set; }

        public void PublishMessage()
        {
            var factory = new ConnectionFactory() { HostName = HostName };

            using var connection = factory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Queue, durable:false, exclusive:false, autoDelete:false, arguments: null);

                string message = Message;

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: Queue, body: body);

                Console.WriteLine("Message published on RabbitMQ");
            }
        }
    }
}
