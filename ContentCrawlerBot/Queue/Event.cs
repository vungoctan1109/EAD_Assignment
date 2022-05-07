using ContentCrawlerBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.Queue
{
    class Event
    {
        public string Url { get; set; }
        public string TitleDetailSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ImageDetailSelector { get; set; }
        public string DescriptionDetailSelector { get; set; }
        public int CategoryId { get; set; }
        public Event()
        {
        }
        public Event(string url, Source source)
        {
            this.Url = url;
            this.TitleDetailSelector = source.TitleDetailSelector;
            this.ContentDetailSelector = source.ContentDetailSelector;
            this.ImageDetailSelector = source.ImageDetailSelector;
            this.DescriptionDetailSelector = source.DescriptionDetailSelector;
            this.CategoryId = source.CategoryId;
        }
    }
}
