using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var url = "https://vnexpress.net/the-thao";
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var nodeList = doc.DocumentNode.QuerySelectorAll("h3.title-news a");
            HashSet<string> setLink = new HashSet<string>();
            foreach (var node in nodeList)
            {
                var link = node.Attributes["href"].Value;
                if (string.IsNullOrEmpty(link))
                {
                    continue;
                }
                setLink.Add(link);
            }
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "VnExpress",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                foreach (string link in setLink)
                {
                    var body = Encoding.UTF8.GetBytes(link);
                    channel.BasicPublish(exchange: "",
                                  routingKey: "VnExpress",
                                  basicProperties: null,
                                  body: body);
                    Console.WriteLine(" [x] Sent {0}", link);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}