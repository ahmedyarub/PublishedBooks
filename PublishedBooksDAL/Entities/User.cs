using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishedBooksDAL.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<string> Demands { get; set; }
    }
}
