using Binbin.Linq;
using PublishedBooksDAL.Repositories;
using PublishedBooksDAL.Entities;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Http;
using PublishedBooks.Infrastructure.Search;

namespace PublishedBooks.Controllers
{
    //[Authorize]
    public class BookController : ApiController
    {
        IRepository<Book> booksRepository;

        public BookController(IRepository<Book> books)
        {
            booksRepository = books;
        }

        // GET api/book
        public IEnumerable<Book> Get()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            if (nvc.Count == 0)
                return booksRepository.GetAll();
            else
            {
                var query = PredicateBuilder.True<Book>();

                query = new BookTitleSearchCriteria<Book>().CheckCriteria(query, nvc["title"]);
                query = new BookDescriptionSearchCriteria<Book>().CheckCriteria(query, nvc["description"]);
                query = new BookPublisherSearchCriteria<Book>().CheckCriteria(query, nvc["publisher"]);
                query = new BookAuthorSearchCriteria<Book>().CheckCriteria(query, nvc["author"]);

                return booksRepository.SearchFor(query);
            }
        }
    }
}
