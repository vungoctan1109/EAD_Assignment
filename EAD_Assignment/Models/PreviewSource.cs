using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAD_Assignment.Models
{
    public class PreviewSource
    {
        public string Link { get; set; } // vnexpress.net/the-thao
        public string LinkSelector { get; set; } // h1 .title-new, Các bài viết chọn trong trang thể thao vnxpess
        public string SubUrl { get; set; } // h1 .title-new, Các bài viết chọn trong trang thể thao vnxpess
        public string TitleDetailSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ImageDetailSelector { get; set; }
        public string DescriptionDetailSelector { get; set; }
    }
}