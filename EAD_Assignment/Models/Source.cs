using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAD_Assignment.Models
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string LinkSelector { get; set; }
        public string TitleDetailSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ImageDetailSelector { get; set; }
        public string DescriptionDetailSelector { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public int Status { get; set; }

        // cho Id Name Link Status vào bảng trang admin
    }
}