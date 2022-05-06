using LinkCrawlerBot.Models;
using LinkCrawlerBot.Queue;
using LinkCrawlerBot.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.App
{
    class CrawlApp
    {
        private SourceService sourceService;
        private HandleQueue myQueue;
        private List<Source> listSource;
        public CrawlApp()
        {
            sourceService = new SourceService();
            myQueue = new HandleQueue();
            listSource = new List<Source>();
        }
        public void Execute()
        {
            listSource = sourceService.GetAll();
            foreach (var source in listSource)
            {
                var listEvent = sourceService.GetSubLink(source);

                foreach (var eventQueue in listEvent)
                {
                    myQueue.Send(eventQueue);
                }
            }
        }

    }
}
