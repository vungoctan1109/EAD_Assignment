    using ContentCrawlerBot.Service;
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
        private ArticleService articleService;
        public HandleQueue()
        {
            articleService = new ArticleService();
        }
        public void Receive()
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
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                Console.WriteLine("Waiting for message.");
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var stuff = Encoding.UTF8.GetString(body);
                    Event eventReceive = JsonConvert.DeserializeObject<Event>(stuff);
                    var article = articleService.GetArticleFromQueue(eventReceive);
                    if (article != null)
                    {
                        articleService.Save(article);
                        Console.WriteLine("[x]Article received:  {0}", article.Url);
                    }
                };
                channel.BasicConsume(queue: "crawler",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}

