using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.Queue
{
    class HandleQueue
    {
        public void Receive(Event eventQueue)
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var i = 0;
                    var message = Encoding.UTF8.GetString(body);
                    Event eventQ = JsonConvert.DeserializeObject<Event>(message);
                    //var article = articleService.GetArticle(eventQueue);
                    //if (article != null)
                    //{
                    //    articleService.Save(article);
                    //    Console.WriteLine(" [x] Received{0}:  {1}", i++, article.Title);
                    //}


                };
                channel.BasicConsume(queue: "crawler",
                                     autoAck: true,
                                     consumer: consumer);
            }     
        }
    }
}
