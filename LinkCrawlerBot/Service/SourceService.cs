using HtmlAgilityPack;
using LinkCrawlerBot.Models;
using LinkCrawlerBot.Queue;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LinkCrawlerBot.Service
{
    class SourceService
    {
        private ArticleService articleService = new ArticleService();
        private readonly string SelectAllQuery = "SELECT * FROM Sources";
        public List<Source> GetAll()
        {
            List<Source> sources = new List<Source>();
            try
            {
                using (var cnn = DbHelper.connect())
                {
                    cnn.Open();
                    var command = new SqlCommand(SelectAllQuery, cnn);
                    var data = command.ExecuteReader();
                    while (data.Read())
                    {
                        var source = new Source()
                        {
                            Id = data.GetInt32(0),
                            Url = data.GetString(2),
                            LinkSelector = data.GetString(3),
                            TitleDetailSelector = data.GetString(4),
                            ContentDetailSelector = data.GetString(5),
                            ImageDetailSelector = data.GetString(6),
                            DescriptionDetailSelector = data.GetString(7),
                            CategoryId = data.GetInt32(10),
                        };
                        sources.Add(source);
                        Console.WriteLine(source.Url);
                    }
                    return sources;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;

            }
        }
        public HashSet<Event> GetSubLink(Source source)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(source.Url);
            var nodeList = doc.QuerySelectorAll(source.LinkSelector);
            HashSet<Event> eventQueues = new HashSet<Event>();
            foreach (var node in nodeList)
            {
                if (node.Attributes["href"] != null)
                {
                    var link = node.Attributes["href"].Value;
                    if (string.IsNullOrEmpty(link) || link.Contains("#box_comment_vne"))
                    {
                        continue;
                    }
                    var existSubUrl = articleService.GetArticleByUrl(link);
                    if (existSubUrl)
                    {
                        continue;
                    }
                    Console.WriteLine(link);
                    Event s = new Event(link, source);
                    eventQueues.Add(s);
                }

            }
            return eventQueues;
        }
    }
}
