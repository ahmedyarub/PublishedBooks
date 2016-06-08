using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublishedBooks.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string[] Authors { get; set; }
    }
}