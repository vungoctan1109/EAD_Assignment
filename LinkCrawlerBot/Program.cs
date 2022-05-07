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
        private static async Task Main(string[] args)
        {
            CronJobApp app = new CronJobApp();
            await app.Run();
        }
    }
}