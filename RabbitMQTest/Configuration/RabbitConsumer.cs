using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace RabbitMQTest.Configuration
{
    public class RabbitConsumer
    {
        public string Queue { get; set; }

        public string HostName { get; set; }

        public void ConsumeMessage()
        {
            var factory = new ConnectionFactory() { HostName = HostName };

            using var connection = factory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    try 
                    {
                      var body = ea.Body.ToArray();
                      var message = Encoding.UTF8.GetString(body);
                        
                      Console.Out.WriteLineAsync($" {message} received from consumer");
                        
                      channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch(Exception ex)
                    {
                        Console.Out.WriteLine($"{ex.Message}");
                        channel.BasicNack(ea.DeliveryTag, false, true);                        
                    }   
                };

                channel.BasicConsume(queue: Queue, autoAck: false, consumer: consumer);

            }
        }
    }
}
