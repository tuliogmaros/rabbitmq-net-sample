
using RabbitMQ.Client;
using RabbitMQTest.Configuration;
using System.Text;


new RabbitPublisher() 
{ 
    Queue = "TestQueue01",
    HostName = "localhost",
    Message = "infinity login is awesome!"
}
.PublishMessage();

new RabbitConsumer()
{
    Queue = "TestQueue01",
    HostName = "localhost"
}
.ConsumeMessage();

