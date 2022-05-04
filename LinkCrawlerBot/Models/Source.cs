using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.Models
{
    internal class Source
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Url { get; set; }
        public string LinkSelector { get; set; }
        public string TitleDetailSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ImageDetailSelector { get; set; }
        public string DescriptionDetailSelector { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public int Status { get; set; }
    }
}