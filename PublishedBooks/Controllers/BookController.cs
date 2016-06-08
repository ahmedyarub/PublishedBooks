using Binbin.Linq;
using PublishedBooksDAL.Repositories;
using PublishedBooksDAL.Entities;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PublishedBooks.Controllers
{
    //[Authorize]
    public class BookController : ApiController
    {
        // GET api/book
        public IEnumerable<Book> Get()
        {
            var booksRepository = new MongoDbRepository<Book>();
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            if (nvc.Count == 0)
                return booksRepository.GetAll();
            else
            {
                var query = PredicateBuilder.True<Book>();

                if(nvc["publisher"]!=null)
                        query = query.And(b => b.Publisher.ToLower().Contains(nvc["publisher"].ToLower()));

                if (nvc["title"] != null)
                    query = query.And(b => b.Title.ToLower().Contains(nvc["title"].ToLower()));

                if (nvc["author"] != null)
                    query = query.And(b => b.Authors.Any(a => a.ToLower().Contains(nvc["author"].ToLower())));

                if (nvc["description"] != null)
                    query = query.And(b => b.Description.ToLower().Contains(nvc["description"].ToLower()));

                return booksRepository.SearchFor(query);
            }
        }
    }
}
