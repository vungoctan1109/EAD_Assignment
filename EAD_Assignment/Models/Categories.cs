using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EAD_Assignment.Models
{
    public class Categories
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //[ForeignKey("Category")] -> Khoa ngoai bang article
        //public ICollection<Articles> Articles { get; set; }

        //Duoi sau cung cua link VD: /the-thao
        public string SubLink { get; set; }
    }
}