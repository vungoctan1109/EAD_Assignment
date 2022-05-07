using LinkCrawlerBot.Queue;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCrawlerBot.App
{
    class SaveArticleApp :IJob
    {
        private HandleQueue handleQueue;
        public SaveArticleApp()
        {
            handleQueue = new HandleQueue();
        }
        public async Task Run()
        {
            handleQueue.Receive();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Run();
        }
    }
}
