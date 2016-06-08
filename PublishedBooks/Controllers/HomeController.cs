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

namespace PublishedBooks.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string booktitle, string description, string publisher, string author, int page = 1)
        {
            ViewBag.Title = "Published Book";
            var client = new HttpClient();

            var builder = new UriBuilder("http://localhost:34717/api/Book");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

            if (!string.IsNullOrEmpty(booktitle))
            {
                query["title"] = booktitle;
                ViewBag.BookTitle = booktitle;
            }
            if (!string.IsNullOrEmpty(publisher))
            {
                query["publisher"] = publisher;
                ViewBag.Publisher = publisher;
            }
            if (!string.IsNullOrEmpty(description))
            {
                query["description"] = description;
                ViewBag.Description = description;
            }
            if (!string.IsNullOrEmpty(author))
            {
                query["author"] = author;
                ViewBag.Author = author;
            }

            builder.Query = query.ToString();

            var response = client.GetAsync(builder.ToString());
            if (response.Result.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<Book>>(response.Result.Content.ReadAsStringAsync().Result);
                return View(new PagedList<Book>(items, page, 5));
            }
            return View();
        }
    }
}
