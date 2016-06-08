using System;
using System.Linq.Expressions;

namespace PublishedBooks.Infrastructure.Search
{
    public interface ISearchCriteria<TEntity>
    {
        Expression<Func<TEntity, bool>> CheckCriteria(Expression<Func<TEntity, bool>> source, string searchitem);
    }
}
