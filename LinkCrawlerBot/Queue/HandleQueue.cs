using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.Queue
{
    class HandleQueue
    {
        public void Send(Event eventQueue)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "crawler",
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventQueue));
                channel.BasicPublish(exchange: "",
                                     routingKey: "crawler",
                                     basicProperties: null,
                                     body: body);
            }    
        }
    }
}
