using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAD_Assignment.Models
{
    public class PreviewSource
    {
        public string Link { get; set; }
        public string LinkSelector { get; set; }
        public string SubUrl { get; set; }
        public string TitleDetailSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ImageDetailSelector { get; set; }
        public string DescriptionDetailSelector { get; set; }
    }
}