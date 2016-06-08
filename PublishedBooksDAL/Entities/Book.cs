using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishedBooksDAL.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string[] Authors { get; set; }
    }
}
