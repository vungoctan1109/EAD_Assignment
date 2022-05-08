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

        [Required]
        public string Link { get; set; }

        [Required]
        public string LinkSelector { get; set; }

        [Required]
        public string TitleDetailSelector { get; set; }

        [Required]
        public string ContentDetailSelector { get; set; }

        [Required]
        public string ImageDetailSelector { get; set; }

        [Required]
        public string DescriptionDetailSelector { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int Status { get; set; }
    }
}