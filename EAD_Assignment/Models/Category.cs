﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EAD_Assignment.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}