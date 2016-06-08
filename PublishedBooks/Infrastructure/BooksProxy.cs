using Newtonsoft.Json;
using PublishedBooks.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PublishedBooks.Infrastructure
{
    public class BooksProxy : IBooksProxy
    {
        public string serverurl { get; } = "http://localhost:34717/api/Book";
        public List<Book> GetFiltered(string title, string description, string publisher, string author)
        {
            var builder = new UriBuilder(serverurl);
            var client = new HttpClient();

            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

            if (!string.IsNullOrEmpty(title))
            {
                query["title"] = title;
            }

            if (!string.IsNullOrEmpty(description))
            {
                query["description"] = title;
            }

            if (!string.IsNullOrEmpty(publisher))
            {
                query["publisher"] = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                query["author"] = title;
            }

            builder.Query = query.ToString();

            var response = client.GetAsync(builder.ToString());
            if (response.Result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Book>>(response.Result.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}