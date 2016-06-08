using Binbin.Linq;
using PublishedBooksDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PublishedBooks.Infrastructure.Search
{
    public class BookPublisherSearchCriteria<TEntity> : ISearchCriteria<TEntity> where TEntity : Book
    {
        public Expression<Func<TEntity, bool>> CheckCriteria(Expression<Func<TEntity, bool>> source, string searchitem)
        {
            if (searchitem == null)
                return source;
            else
                return source.And(b => b.Publisher.ToLower().Contains(searchitem.ToLower()));
        }
    }
}