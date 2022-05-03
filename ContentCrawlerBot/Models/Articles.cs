using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCrawlerBot.Models
{
    internal class Articles
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public int Category { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}