using PublishedBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishedBooks.Infrastructure
{
    interface IBooksProxy
    {
        List<Book> GetFiltered(string title, string description, string publisher, string author);
    }
}
