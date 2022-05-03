using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCrawlerBot.Models
{
    internal class Sources
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Link { get; set; }
        public string LinkSelector { get; set; }
        public string TitleDetailSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ImageDetailSelector { get; set; }
        public string RemoveSelector { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}