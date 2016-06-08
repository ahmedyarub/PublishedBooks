using Newtonsoft.Json;
using PublishedBooks.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PublishedBooks.Infrastructure;

namespace PublishedBooks.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string booktitle, string description, string publisher, string author, int page = 1)
        {
            var proxy = new BooksProxy();

            ViewBag.Title = "Published Book";

            if (!string.IsNullOrEmpty(booktitle))
            {
                ViewBag.BookTitle = booktitle;
            }
            if (!string.IsNullOrEmpty(publisher))
            {
                ViewBag.Publisher = publisher;
            }
            if (!string.IsNullOrEmpty(description))
            {
                ViewBag.Description = description;
            }
            if (!string.IsNullOrEmpty(author))
            {
                ViewBag.Author = author;
            }

            var items = proxy.GetFiltered(booktitle, description, publisher, author);

            return View(new PagedList<Book>(items, page, 5));
        }
    }
}
