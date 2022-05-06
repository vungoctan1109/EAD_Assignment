using HtmlAgilityPack;
using LinkCrawlerBot.App;
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
            var app = new CrawlApp();
            app.Execute();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}