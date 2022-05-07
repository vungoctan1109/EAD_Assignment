using ContentCrawlerBot.App;
using ContentCrawlerBot.Models;
using ContentCrawlerBot.Service;
using HtmlAgilityPack;
using LinkCrawlerBot.Queue;
using Newtonsoft.Json;
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
        private static async Task Main(string[] args)
        {
            CronJobApp app = new CronJobApp();
            await app.Run();
        }
    }
}
