using ContentCrawlerBot.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCrawlerBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var cnn = DbHelper.connect())
            {
                cnn.Open();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "VnExpress",
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
                        var link = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", link);
                        var web = new HtmlWeb();
                        HtmlDocument doc = web.Load(link);
                        var articleTitle = doc.DocumentNode.QuerySelector("h1.title-detail").InnerText;
                        var articleContent = doc.DocumentNode.QuerySelector("p.description").InnerText;
                        var articleImage = doc.DocumentNode.QuerySelector("img").Attributes["src"].Value;
                        var article = new Article()
                        {
                            Url = link,
                            Title = articleTitle,
                            Detail = articleContent,
                            ImageUrl = articleImage,
                            UpdatedAt = DateTime.Now,
                            CreatedAt = DateTime.Now
                        };
                        if (article.Url != DbHelper.checkExistUrl(article.Url, cnn))
                        {
                            SqlCommand command = new SqlCommand("INSERT INTO Articles (Url, Title, Detail, ImageUrl, CategoryId, CreatedAt, UpdatedAt, Status) VALUES (@url, @title, @detail, @image, 1, @createdAt, @updatedAt, 1)", cnn);
                            command.Prepare();
                            command.Parameters.AddWithValue("@url", article.Url);
                            command.Parameters.AddWithValue("@title", article.Title);
                            command.Parameters.AddWithValue("@detail", article.Detail);
                            command.Parameters.AddWithValue("@image", article.ImageUrl);
                            command.Parameters.AddWithValue("@createdAt", article.CreatedAt);
                            command.Parameters.AddWithValue("@updatedAt", article.UpdatedAt);
                            command.ExecuteNonQuery();
                            Console.WriteLine("Added article {0} to database.", article.Title);
                        }
                    };
                    channel.BasicConsume(queue: "VnExpress",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}